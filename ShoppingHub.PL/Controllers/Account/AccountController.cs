using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Common;
using ShoppingHub.BLL.Helper;
using ShoppingHub.BLL.ModelVm.UserVM;
using ShoppingHub.DAL.Entities;

namespace ShoppingHub.PL.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IEmailSender emailSender;
        private readonly UserManager<User> userManger;
        private readonly SignInManager<User> signInManager;
        public AccountController(UserManager<User> userManger, SignInManager<User> signInManager, IEmailSender emlSender)
        {
            this.userManger = userManger;
            this.signInManager = signInManager;
            this.emailSender = emlSender;
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
            var user = new User(Role.USER, Load.UploadFile("Files/images/usersImages",
                usr.profileImage!), usr.Address, usr.createdOn.ToString())
            {
                UserName = usr.Name,
                Email = usr.Email,
                PhoneNumber = usr.PhoneNumber,
            };
            IdentityResult? result = await userManger.CreateAsync(user, usr.Password);

            if (result.Succeeded)
            {
                var resultRole = await userManger.AddToRoleAsync(user, Role.USER);

                var link = await userManger.GenerateEmailConfirmationTokenAsync(user);

                var confirmationLink = Url.Action("ConfirmEmail", "Account",
                    new { userId = user.Id, token = link }, Request.Scheme);
                BackgroundJob.Enqueue(() => emailSender.SendEmailAsync(user.Email, "Confirm your email",
                   $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>."));
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
            if (!ModelState.IsValid)
            {
                return View(usr);
            }
            var result = await signInManager.PasswordSignInAsync(usr.UserName, usr.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("GetAllProducts", "Product");
            }
            else
            {              
                ViewBag.ErrorMessage = "Invalid UserName Or Password";
                return View(usr);

            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
                return BadRequest();

            var user = await userManger.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var result = await userManger.ConfirmEmailAsync(user, token);
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            // Get the current user
            var currentUser = await this.userManger.GetUserAsync(User);
            UserProfileVM result = new UserProfileVM();
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                result.Address = currentUser.Address;
                result.phoneNumber = currentUser.PhoneNumber!;
                result.userName = currentUser.UserName!;
                result.totalOrders = currentUser.Orders.Count;
                result.email = currentUser.Email!;
                result.userImage = currentUser.ImagePath;
            }


            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            // Get the current user
            var currentUser = await this.userManger.GetUserAsync(User);
            EditUserVM result = new EditUserVM();
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                result.Address = currentUser.Address;
                result.PhoneNumber = currentUser.PhoneNumber!;
                result.Name = currentUser.UserName!;
                result.Email = currentUser.Email!;
                result.oldProfileImage = currentUser.ImagePath;
            }
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditUserVM usr)
        {
            // Temporarily use full qualified name to test
            var currentUser = await this.userManger.GetUserAsync(User) as ShoppingHub.DAL.Entities.User;
            if (currentUser == null)
            {
                return NotFound("User not found");
            }

            // Update phone number
            if (usr.PhoneNumber != currentUser.PhoneNumber)
            {
                var phoneResult = await userManger.SetPhoneNumberAsync(currentUser, usr.PhoneNumber);
            }
            if (usr.Email != currentUser.Email)
            {
                var emailToken = await userManger.GenerateChangeEmailTokenAsync(currentUser, usr.Email);
                var emailResult = await userManger.ChangeEmailAsync(currentUser, usr.Email, emailToken);

            }
            bool test1 = false;
            if (usr.newProfileImage != null)
            {
                if (usr.oldProfileImage != null)
                {
                    var test = Load.RemoveFile("Files/images/usersImages", usr.oldProfileImage);
                    Console.WriteLine(test);
                    test1 = true;
                }
            }
            Console.WriteLine(test1);
            currentUser.updateProfile(usr.Name, usr.newProfileImage != null ? Load.UploadFile("Files/images/usersImages", usr.newProfileImage) : usr.oldProfileImage, usr.Address);


            var updateResult = await userManger.UpdateAsync(currentUser);


            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToAction("Profile");



        }
        /*   [HttpGet]
           public async Task<IActionResult> ForgetPassword()
           {
               return View();
           }*/

        /*
                [HttpPost]*/
        /*  public async Task<IActionResult> ForgetPassword(string email)
          {
              var user = await userManger.FindByEmailAsync(email);

              if (user != null)
              {
                  var token = await userManger.GeneratePasswordResetTokenAsync(user);

                  var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = email, Token = token }, Request.Scheme);

                   MailSender.Mail("Password Reset", passwordResetLink);

                  //logger.Log(LogLevel.Warning, passwordResetLink);

                  return RedirectToAction("ConfirmForgetPassword");
              }

              return RedirectToAction("ConfirmForgetPassword");
          }
          [HttpGet]*/
        /* public async Task<IActionResult> ResetPassword()
         {
             return View();
         }
 */

        /*     [HttpPost]*/
        /*  public async Task<IActionResult> ResetPassword(string email)
          {


          }*/

        [HttpGet]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = userManger.Users.ToList();
            
            return View(users);
        }
        [HttpPost]
        [Authorize(Roles = Role.ADMIN)]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> MakeAdmin(string userId)
        {
            var user = await userManger.FindByIdAsync(userId);
            
            if (user != null)
            {
                Console.WriteLine("Wahhhh");
                await userManger.RemoveFromRoleAsync(user,Role.USER);
                await userManger.AddToRoleAsync(user, Role.ADMIN);
                user.changeRole(Role.ADMIN);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }
}

