using Business.Services.Abstract.Users;
using Business.ViewModels.Users;
using Business.ViewModels.Users.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }
        public async Task<IActionResult> Index(int id)
        {
            var model = new HomeIndexVM
            {
                Sliders = await _homeService.GetSlidersAsync(),
                Categories = await _homeService.GetCategoryAsync(),
                CategoryTake3 = await _homeService.GetCategoryWithSubcategoryAsync(),
                FilteredProducts = await _homeService.GetProductsByCategoryAsync(id),
                AllProducts = await _homeService.GetProductsAsync(),
                onSaleComponents = await _homeService.GetOnSaleComponentsAsync(),
                onSale_2Components = await _homeService.GetOnSale_2ComponentsAsync(),
                Blogs = await _homeService.GetBlogsAsync(),
                WishlistProducts = await _homeService.GetWishlistProductsAsync(User),
                BasketProducts = await _homeService.GetBasketProductsAsync(User),
            };

            return View(model);
        }

        public async Task<IActionResult> TabMenu(int id)
        {
            var model = new HomeTabMenuVM
            {
                Products = await _homeService.GetProductsByCategoryAsync(id)
            };

            return PartialView("_TabMenuContentPartial", model.Products);
        }


        public static class EnumHelper
        {
            public static string GetEnumDisplayName(Enum value)
            {
                var fieldInfo = value.GetType().GetField(value.ToString());
                var attributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
                var output = attributes.Length > 0 && !string.IsNullOrEmpty(attributes[0].Name) ? attributes[0].Name : value.ToString();
                return output;
            }
        }

    }
}
