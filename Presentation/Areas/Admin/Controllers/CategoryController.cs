using Business.Services.Abstract.Admin;
using Business.Services.Concrete.Admin;
using Business.ViewModels.Admin.Category;
using Business.ViewModels.Admin.Slider;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM model)
        {
            var isSucceded = await _categoryService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var newmodel = await _categoryService.GetUpdateAsync(id);
            if (newmodel == null) return NotFound("Category Not Found!");

            return View(newmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, CategoryUpdateVM model)
        {
            var isSucceded = await _categoryService.PostUpdateAsync(id, model);

            if (isSucceded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _categoryService.DeleteAsync(id);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return NotFound("Unexpected error has occurred");
        }


    }
}
