using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingHub.BLL.ModelVm.Cart;
using ShoppingHub.BLL.Services.Abstraction;
using ShoppingHub.DAL.Entities;

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
        [Authorize]
        public IActionResult AddToCart( CartItemVM cartItem)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = _cartService.AddToCart(userId, cartItem);
            if (result.Item1 == true)
                ViewBag.Message = result.Item2;
            return RedirectToAction("ViewCart");
        }
        [HttpPost]
        public IActionResult UpdateQuantity(int productID, int quantity)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = _cartService.UpdateQuantity(productID, userId, quantity);
            if (result.Item1 == true)
                ViewBag.Message = result.Item2;
            return RedirectToAction("ViewCart");
        }
        [HttpPost]
        public IActionResult RemoveItem(int productID)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = _cartService.RemoveItem(userId, productID);
            if (result.Item1 == true)
                ViewBag.Message = result.Item2;
            return RedirectToAction("ViewCart");
        }
        [HttpPost]
        public IActionResult ClearCart()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = _cartService.ClearCart(userId);
            if (result.Item1 == true)
                ViewBag.Message = result.Item2;
            return RedirectToAction("ViewCart");
        }
        [HttpGet]
        public IActionResult ViewCart()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
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
