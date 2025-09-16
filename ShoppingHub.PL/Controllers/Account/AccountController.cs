using Microsoft.AspNetCore.Mvc;
using ShoppingHub.BLL.ModelVM;
using ShoppingHub.BLL.Service.Abstraction;
using ShoppingHub.BLL.Service.Implementaion;

namespace ShoppingHub.PL.Controllers.Account
{
    public class AccountController : Controller
    {
        IUserService userService;
        
        public AccountController(IUserService _service)
        {
            userService = _service;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(CreateUserVM user)
        {
            if (!ModelState.IsValid)
            {
               
                return View(user);

            }

            var result = userService.Create(user);
            Console.WriteLine(result.Item1);
            if (result.Item1)
            {
                ViewBag.ErrorMessage = result.Item2;
                return View(user);
            }

            return RedirectToAction("index", "Home");
        }
    }
}
