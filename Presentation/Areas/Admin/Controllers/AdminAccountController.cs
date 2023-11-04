using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.Account;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AdminAccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginVM model)
        {
            var isSucceded = await _accountService.LoginAsync(model);

            if (isSucceded) return RedirectToAction(nameof(Index), "Dashboard");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();

            return RedirectToAction(nameof(Login), "adminaccount");
        }
    }
}
