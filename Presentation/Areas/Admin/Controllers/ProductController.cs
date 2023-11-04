
using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.Product;
using Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Superadmin, Admin, HR")]

    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(ProductIndexVM model)
        {
            return View(await _productService.IndexGetAllAsync(model));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await _productService.GetCreateAsync();
            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {
            var isSucceded = await _productService.PostCreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var newmodel = await _productService.GetUpdateAsync(id);
            if (newmodel is null) return NotFound();

            return View(newmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, ProductUpdateVM model)
        {
            var isSucceded = await _productService.PostUpdateAsync(id, model);
            if (isSucceded) return RedirectToAction(nameof(Index));

            model = await _productService.GetUpdateAsync(id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _productService.DeleteAsync(id);
            if (isSucceded) return RedirectToAction(nameof(Index));

            return NotFound();
        } 

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _productService.DetailsAsync(id);
            if (model is null) return RedirectToAction(nameof(Index));
            
            return View(model);
        }

    }
}
