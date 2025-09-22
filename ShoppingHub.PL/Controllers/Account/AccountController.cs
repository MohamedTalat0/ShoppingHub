using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using ShoppingHub.BLL.Helper;
using ShoppingHub.BLL.ModelVm;
using ShoppingHub.BLL.ModelVM;
using ShoppingHub.BLL.Service.Implementaion;
using ShoppingHub.DAL.Entities;
using ShoppingHub.Serviese;

namespace ShoppingHub.PL.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> userManger;
        private readonly SignInManager<User> signInManager;
        public AccountController(UserManager<User> userManger, SignInManager<User> signInManager,UserService userService)
        {
            this.userManger = userManger;
            this.signInManager = signInManager;
            this._userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(CreateUserVM usr)
        {
            if (!ModelState.IsValid)
            {

                return View(usr);

            }
            var user = new User(Role.USER, Load.UploadFile("Files/images/usersImages", usr.profileImage!), usr.Address, usr.createdOn.ToString())
            {
                UserName =usr.Name,
                Email =usr.Email,
                PhoneNumber = usr.PhoneNumber,
            };
            IdentityResult? result = await userManger.CreateAsync(user, usr.Password);

            if (result.Succeeded)
            {
                var resultRole = await userManger.AddToRoleAsync(user, Role.USER);
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("Password", item.Description);
                }
            }
            return View(usr);
          
        }


        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserVM usr)
        {
            var result = await signInManager.PasswordSignInAsync(usr.UserName, usr.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid UserName Or Password";
                return View(usr);

            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            // Get the current user
            var currentUser = await this.userManger.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Get user profile using the current user's ID
            var result = _userService.getUser(currentUser.Id);

            if (result.Item1)
            {
                ViewBag.ErrorMessage = result.Item2;
                return View();
            }
            else
            {
                return View(result.Item3);
            }
        }

        }
    }
