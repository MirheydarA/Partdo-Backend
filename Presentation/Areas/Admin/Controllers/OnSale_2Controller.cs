using Business.Services.Abstract.Admin;
using Business.Services.Concrete.Admin;
using Business.ViewModels.Admin.Category;
using Business.ViewModels.Admin.OnSale_1;
using Business.ViewModels.Admin.OnSale_2;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OnSale_2Controller : Controller
    {
        private readonly IOnSale_2Service _onSale_2Service;

        public OnSale_2Controller(IOnSale_2Service onSale_2Service)
        {
            _onSale_2Service = onSale_2Service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _onSale_2Service.IndexGetAllAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OnSale_2CreateVM model)
        {
            var isSucceded = await _onSale_2Service.PostCreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var newmodel = await _onSale_2Service.GetUpdateAsync(id);
            if (newmodel == null) return NotFound("OnSale Component Not Found!");

            return View(newmodel);
        }


        [HttpPost]
        public async Task<IActionResult> Update(int id, OnSale_2UpdateVM model)
        {
            var isSucceded = await _onSale_2Service.PostUpdateAsync(id, model);

            if (isSucceded) return RedirectToAction(nameof(Index));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _onSale_2Service.DeleteAsync(id);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return NotFound("Unexpected error has occurred");
        }


    }
}