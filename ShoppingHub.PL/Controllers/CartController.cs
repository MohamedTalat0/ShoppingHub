using Microsoft.AspNetCore.Mvc;
using ShoppingHub.BLL.ModelVm.Cart;
using ShoppingHub.BLL.Services.Abstraction;

namespace ShoppingHub.PL.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            return ViewCart();
        }
        [HttpPost]
        public IActionResult AddToCart( CartItemVM cartItem)
        {
            var userId = "aad01031-98e7-494d-ab6b-cba72be90dab";
            var result = _cartService.AddToCart(userId, cartItem);
            if (result.Item1 == true)
                ViewBag.Message = result.Item2;
            return RedirectToAction("");
        }
        [HttpPost]
        public IActionResult UpdateQuantity(int productID, int quantity)
        {
            var userId = "aad01031-98e7-494d-ab6b-cba72be90dab";
            var result = _cartService.UpdateQuantity(productID, userId, quantity);
            if (result.Item1 == true)
                ViewBag.Message = result.Item2;
            return RedirectToAction("ViewCart");
        }
        [HttpPost]
        public IActionResult RemoveItem(int productID)
        {
            var userId = "aad01031-98e7-494d-ab6b-cba72be90dab";
            var result = _cartService.RemoveItem(userId, productID);
            if (result.Item1 == true)
                ViewBag.Message = result.Item2;
            return RedirectToAction("ViewCart");
        }
        [HttpPost]
        public IActionResult ClearCart()
        {
            var userId = "aad01031-98e7-494d-ab6b-cba72be90dab";
            var result = _cartService.ClearCart(userId);
            if (result.Item1 == true)
                ViewBag.Message = result.Item2;
            return RedirectToAction("ViewCart");
        }
        [HttpGet]
        public IActionResult ViewCart()
        {
            var userId = "aad01031-98e7-494d-ab6b-cba72be90dab";
            var result = _cartService.ViewCart(userId);
            if (result.Item1 == true)
            {
                ViewBag.ErrorMessage = result.Item2;
                ViewBag.CartCount = 0;
            }
            else
            {
                ViewBag.CartCount = result.Item3.TotalItems;
            }
            return View("Index", result.Item3);
        }

    }
}
