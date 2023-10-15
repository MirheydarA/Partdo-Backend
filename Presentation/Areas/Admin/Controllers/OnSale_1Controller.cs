using Business.Services.Abstract.Admin;
using Business.Services.Concrete.Admin;
using Business.ViewModels.Admin.Category;
using Business.ViewModels.Admin.OnSale_1;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OnSale_1Controller : Controller
    {
        private readonly IOnSale_1Service _onSale_1Service;

        public OnSale_1Controller(IOnSale_1Service onSale_1Service)
        {
            _onSale_1Service = onSale_1Service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _onSale_1Service.IndexGetAllAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OnSale_1CreateVM model)
        {
            var isSucceded = await _onSale_1Service.PostCreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var newmodel = await _onSale_1Service.GetUpdateAsync(id);
            if (newmodel == null) return NotFound("OnSale Component Not Found!");

            return View(newmodel);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int id, OnSale_1UpdateVM model)
        {
            var isSucceded = await _onSale_1Service.PostUpdateAsync(id, model);

            if (isSucceded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _onSale_1Service.DeleteAsync(id);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return NotFound("Unexpected error has occurred");
        }


    }
}
