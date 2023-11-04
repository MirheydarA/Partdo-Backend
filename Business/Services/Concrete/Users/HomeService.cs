using Business.Services.Abstract.Users;
using Business.ViewModels.Users.Home;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Abstract.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Users
{
    public class HomeService : IHomeService
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly DataAccess.Repositories.Abstract.Users.ICategoryRepository _categoryRepository;
        private readonly DataAccess.Repositories.Abstract.Admin.IProductRepository _productRepository;
        private readonly IOnSale_1Repository _onSale_1Repository;
        private readonly IOnSale_2Repository _onSale_2Repository;
        private readonly IBlogRepository _blogRepository;
        private readonly IWishlistProductRepository _wishlistProductRepository;
        private readonly IBasketProductRepository _basketProductRepository;
        private readonly UserManager<User> _userManager;

        public HomeService(ISliderRepository sliderRepository,
                           DataAccess.Repositories.Abstract.Users.ICategoryRepository categoryRepository,
                           DataAccess.Repositories.Abstract.Admin.IProductRepository productRepository,
                           IOnSale_1Repository onSale_1Repository,
                           IOnSale_2Repository onSale_2Repository,
                           IBlogRepository blogRepository,
                           IWishlistProductRepository wishlistProductRepository,
                           IBasketProductRepository basketProductRepository,
                           UserManager<User> userManager)
        {
            _sliderRepository = sliderRepository;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _onSale_1Repository = onSale_1Repository;
            _onSale_2Repository = onSale_2Repository;
            _blogRepository = blogRepository;
            _wishlistProductRepository = wishlistProductRepository;
            _basketProductRepository = basketProductRepository;
            _userManager = userManager;
        }

        public async Task<List<Category>> GetCategoryWithSubcategoryAsync()
        {
            var categories = await _categoryRepository.GetCategoriesWithSubCategoryAsync();
            return categories;
        }

        public async Task<List<Category>> GetCategoryAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories;
        }

        public async Task<List<Slider>> GetSlidersAsync()
        {
            var sliders = await _sliderRepository.GetAllAsync();
            return sliders;
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(int id)
        {
            var products = await _productRepository.FilterByIdAsync(id);
            return products;
        }
        public async Task<List<Product>> GetProductsAsync()
        {

            var products = await _productRepository.GetAllAsync();
            return products;
        }
        public async Task<List<Product>> GetProductsIncludedAsync(int id)
        {

            var products = await _productRepository.GetProductWithIncludeByCategoryIdTake5(id);
            return products;
        }

        public async Task<List<OnSale_1Component>> GetOnSaleComponentsAsync()
        {
            var onSale_1Components = await _onSale_1Repository.GetAllAsync();
            return onSale_1Components;
        }
        public async Task<List<OnSale_2Component>> GetOnSale_2ComponentsAsync()
        {
            var onSale_2Components = await _onSale_2Repository.GetAllAsync();
            return onSale_2Components;
        }

        public Task<List<Blog>> GetBlogsAsync()
        {
            var blogs = _blogRepository.GetAllAsync();
            return  blogs;
        }

        public async Task<List<WishlistProduct>> GetWishlistProductsAsync(ClaimsPrincipal user)
        {
            var authUser = await _userManager.GetUserAsync(user);
            if (authUser == null) return null;
            var wishlistProducts = await _wishlistProductRepository.GetWishlistProductsByUser(authUser);
            return wishlistProducts;
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _productRepository.GetProductWithIncludeById(id);
            return product;
        }

        public async Task<List<BasketProduct>> GetBasketProductsAsync(ClaimsPrincipal user)
        {
            var authUser = await _userManager.GetUserAsync(user);
            if (authUser == null) return null;
            var basketProducts = await _basketProductRepository.GetBasketProductsByUser(authUser);
            return basketProducts;
        }
    }
}
