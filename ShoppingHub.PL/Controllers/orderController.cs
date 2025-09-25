using Microsoft.AspNetCore.Mvc;
using ShoppingHub.BLL.ModelVm.order;
using ShoppingHub.BLL.Services.Abstraction;
using ShoppingHub.BLL.Services.Implementation;
using ShoppingHub.DAL.Entities;

namespace ShoppingHub.PL.Controllers
{
    public class orderController : Controller
    {
        private readonly IOrderService _orderservice;
        private readonly ICartService _cartService;

        public orderController(IOrderService _service, ICartService cartService)
        {
            _orderservice = _service;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult confirmOrder()
        {
            var userID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var cartItems = _cartService.GetAllUserItems(userID).Item3;
            var order = new CreateOrderVM
            {
                Address = new Address(),
                CartItems = cartItems 
            };
            return View(order);
        }
        [HttpPost]
        public IActionResult confirmOrder(CreateOrderVM order)
        {
            var userID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            //var cartItems=_cartService.GetAllUserItems(userID).Item3;
            var cartItems = _cartService.GetAllUserItems(userID).Item3;
            order.CartItems = cartItems;
           
            if (!ModelState.IsValid)
            {
                return View(order);
            }
           _orderservice.create(order,userID);
            _cartService.ClearCart(userID);
            return View();
        }

        [HttpGet]
        public IActionResult showAllOrders() {
            var orders = _orderservice.getAll();
            //if (orders.Item1)
            return View(orders.Item3);
            //return orders.Item2;
        }
        
        [HttpPost]
        public IActionResult updateStatus(int orderid,string status)
        {
            var userID = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result =_orderservice.updatStatus(orderid,status,userID);
            return RedirectToAction("showAllOrders");
        }

        [HttpGet]
        public IActionResult MyOrders()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var orders = _orderservice.GetUserOrders(userId) ?? new List<OrderVM>();

            return View("MyOrders", orders);
        }

        [HttpPost]
        public IActionResult CancelOrder(int orderId)
        {

            // محاولة إلغاء الطلبية
            var success = _orderservice.cancelOrder(orderId);

            if (success.Item1)
            {
                TempData["Message"] = "The order has been canceled";
            }
            else
            {
                TempData["Error"] = "The order can't been canceled";
            }

            return RedirectToAction("MyOrders");
        }

    }


}
    

