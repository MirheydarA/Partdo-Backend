using Business.Services.Abstract.Users;
using Business.ViewModels.Users.Shop;
using Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;
        private readonly IWishlistService _wishlistService;
        private readonly IHomeService _homeService;

        public ShopController(IShopService shopService,
                              IWishlistService wishlistService,
                              IHomeService homeService)
        {
            _shopService = shopService;
            _wishlistService = wishlistService;
            _homeService = homeService;
        }
        public async Task<IActionResult> Index(ShopIndexVM model)
        {
            var listWishlistProducts = await _homeService.GetWishlistProductsAsync(User);
            ViewBag.ListWishlistProducts = listWishlistProducts;

            return View(await _shopService.IndexGetAllAsync(model));
        }
    }
}
