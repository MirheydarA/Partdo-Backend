using Business.Services.Abstract.Users;
using Business.ViewModels.Users.Basket;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _basketService.IndexGetBasket(User));
        }

        [HttpGet]
        public async Task<IActionResult> AddAsync(BasketVM model)
        {
            var isSucceded = await _basketService.AddAsync(User, model.Id);
            if (!isSucceded) return RedirectToAction(nameof(Index), "NotFound");

            return PartialView(model);
        }
    }
}
