using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.SubCategory;
using Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Superadmin, Admin, HR")]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _subCategoryService.IndexGetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await _subCategoryService.GetCreateAsync();
            if (model is null) return NotFound();
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubCategoryCreateVM model)
        {
            var isSucceded = await _subCategoryService.PostCreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var newmodel = await _subCategoryService.GetUpdateAsync(id);
            if (newmodel is null) return NotFound();

            return View(newmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SubCategoryUpdateVM model, int id)
        {
            var isSucceded = await _subCategoryService.PostUpdateAsync(id,model);
            if (isSucceded) RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _subCategoryService.DeleteAsync(id);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return NotFound();

        }
    }
}
