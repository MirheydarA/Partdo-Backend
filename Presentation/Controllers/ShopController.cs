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
                              IBasketService basketService,
                              IHomeService homeService)
        {
            _shopService = shopService;
            _wishlistService = wishlistService;
            _homeService = homeService;
        }

        public async Task<IActionResult> Index(ShopIndexVM model)
        {
            var listWishlistProducts = await _homeService.GetWishlistProductsAsync(User);
            var listBasketProduct = await _homeService.GetBasketProductsAsync(User);

            ViewBag.ListWishlistProducts = listWishlistProducts;
            ViewBag.ListBasketProducts = listBasketProduct;

            var productList = await _shopService.IndexGetAllAsync(model);
            return View(productList);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _homeService.GetProductById(id);

            var listProducts = await _homeService.GetProductsIncludedAsync(product.SubCategory.CategoryId);
            ViewBag.ListProducts = listProducts;

            var model = await _shopService.DetailsAsync(id);
            if (model is not null) return View(model);

            return RedirectToAction(nameof(Index), "NotFound");
        }
    }
}
