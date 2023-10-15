using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.User;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _userService.IndexGetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await _userService.GetCreateAsync();
            if (model is null) return NotFound();
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateVM model)
        {
            var isSucceded = await _userService.PostCreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var newmodel = await _userService.GetUpdateAsync(id);
            if (newmodel is null) return RedirectToAction(nameof(Index));

            return View(newmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, UserUpdateVM model)
        {
            var isSucceded = await _userService.PostUpdateAsync(id, model);
            if (isSucceded) return RedirectToAction(nameof(Index));

            model = await _userService.GetUpdateAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var isSucceded = await _userService.DeleteAsync(id);
            if (isSucceded) return RedirectToAction(nameof(Index));
            
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var model = await _userService.DetailsAsync(id);

            if (model is null) return RedirectToAction(nameof(Index), "dashboard");
            
            return View(model);
        }

    }
}
