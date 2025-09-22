using Microsoft.AspNetCore.Mvc;
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
        public IActionResult UpdateQuantity(int productID, int quantity)
        {
            var userId = "test-user-id";
            var result = _cartService.UpdateQuantity(productID, userId, quantity);
            if (result.Item1 == true)
                ViewBag.Message = result.Item2;
            return RedirectToAction("ViewCart");
        }
        [HttpPost]
        public IActionResult RemoveItem(int productID)
        {
            var userId = "test-user-id";
            var result = _cartService.RemoveItem(userId, productID);
            if (result.Item1 == true)
                ViewBag.Message = result.Item2;
            return RedirectToAction("ViewCart");
        }
        [HttpPost]
        public IActionResult ClearCart()
        {
            var userId = "test-user-id";
            var result = _cartService.ClearCart(userId);
            if (result.Item1 == true)
                ViewBag.Message = result.Item2;
            return RedirectToAction("ViewCart");
        }
        [HttpGet]
        public IActionResult ViewCart()
        {
            var userId = "test-user-id";
            var result = _cartService.ViewCart(userId);
            if (result.Item1 == true)
                ViewBag.Message = result.Item2;
            return View("Index", result.Item3);
        }

    }
}
