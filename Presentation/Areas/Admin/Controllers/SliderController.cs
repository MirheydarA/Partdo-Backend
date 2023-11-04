using Business.Services.Abstract.Admin;
using Business.Services.Concrete;
using Business.ViewModels.Admin.Slider;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Superadmin, Admin, HR")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            return View(await _sliderService.GetAllAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateVM model)
        {
            var isSucceded = await _sliderService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("Index");

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var newModel = await _sliderService.GetUpdateAsync(id);

            

            if (newModel is null) return NotFound("Slider Not Found!");



            return View(newModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id,  SliderUpdateVM model)
        {
            var isSucceded = await _sliderService.PostUpdateAsync(id, model);

            if (isSucceded) return RedirectToAction(nameof(Index));
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _sliderService.DeleteAsync(id);
            if (isSucceded) return RedirectToAction(nameof(Index));

            return NotFound("Unexpected error has occurred");
        }
    }
}
