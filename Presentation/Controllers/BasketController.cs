using Business.Services.Abstract.Users;
using Business.Services.Concrete.Users;
using Business.ViewModels.Users.Basket;
using Common.Constants;
using Common.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IHomeService _homeService;

        public BasketController(IBasketService basketService, IHomeService homeService)
        {
            _basketService = basketService;
            _homeService = homeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listWishlistProducts = await _homeService.GetWishlistProductsAsync(User);
            ViewBag.ListWishlistProducts = listWishlistProducts;

            return View(await _basketService.IndexGetBasket(User));
        }

        [HttpGet]
        public async Task<IActionResult> AddAsync(BasketVM model)
        {
            if (!User.IsInRole(Roles.User.ToString()))
            {
                //return Unauthorized("asdasdasdadad");
                //throw new Exception("sfmskjgvnd");
                return RedirectToAction(nameof(Index), "Account");
            }
            var isSucceded = await _basketService.AddAsync(User, model.Id);
            if (!isSucceded) return RedirectToAction(nameof(Index), "NotFound");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Increase(BasketVM model)
        {
            var isSucceded = await _basketService.IncreaseAsync(User, model.Id);
            if (!isSucceded) return RedirectToAction(nameof(Index), "NotFound");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Decrease(BasketVM model)
        {
            var isSucceded = await _basketService.DecreaseAsync(User, model.Id);
            if (!isSucceded) return RedirectToAction(nameof(Index), "NotFound");

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(BasketVM model)
        {
            var isSucceded = await _basketService.DeleteAsync(User, model.Id);
            if (!isSucceded) return RedirectToAction(nameof(Index), "NotFound");

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            if (!User.IsInRole(Roles.User.ToString()))
            {
                //return Unauthorized("asdasdasdadad");
                //throw new Exception("sfmskjgvnd");
                return RedirectToAction(nameof(Index), "Account");
            }
            var isSucceded = await _basketService.DeleteAllAsync(User);
            if (!isSucceded) return RedirectToAction(nameof(Index), "NotFound");

            return RedirectToAction(nameof(Index));
        }
    }
}
