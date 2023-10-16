using Business.Services.Abstract.Users;
using Business.ViewModels.Users.Account;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
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
            if (isSucceded) return RedirectToAction(nameof(Login));

            return View();
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
            return RedirectToAction(nameof(Login));
        }
    }
}
