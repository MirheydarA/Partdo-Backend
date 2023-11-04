using Business.Services.Abstract.Users;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Users.Account;
using Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;

        public AccountController(IAccountService accountService,
                                 UserManager<User> userManager,
                                 IEmailSender emailSender)
        {
            _accountService = accountService;
            _userManager = userManager;
            _emailSender = emailSender;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterVM model)
        {
           

            var isSucceded = await _accountService.RegisterAsync(model);
            if (!isSucceded) return View();

            var user = await _userManager.FindByEmailAsync(model.Email); // Assuming Email is the user's email

            if (user != null)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);

                var message = new Business.Services.Utilities.Message(new string[] { user.Email }, "Confirmation email link", confirmationLink);
                _emailSender.SendEmail(message);

                return RedirectToAction(nameof(Login), "Account");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return View("Error");
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? nameof(Login) : "Error");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginVM model)
        {
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            var isSucceded = await _accountService.LoginAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index), "Home");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(AccountForgotPasswordVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return RedirectToAction();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetLink = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);

            var message = new Business.Services.Utilities.Message(new string[] { user.Email }, "Reset password", resetLink);

            _emailSender.SendEmail(message);
            return Content("Your Email Sent");
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new AccountResetPasswordVM
            {
                Email = email,
                Token = token
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(AccountResetPasswordVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Content("User is not found");

            var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }
            return RedirectToAction(nameof(Login));
            //return Content("Password changed Succesfully");
        }
    }
}
