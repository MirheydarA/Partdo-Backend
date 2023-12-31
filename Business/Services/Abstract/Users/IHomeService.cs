﻿using Business.ViewModels.Users.Blog;
using Business.ViewModels.Users.Home;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Users
{
    public interface IHomeService
    {
        Task<List<Slider>> GetSlidersAsync();
        Task<List<Category>> GetCategoryAsync();
        Task<List<Category>> GetCategoryWithSubcategoryAsync();
        Task<List<Product>> GetProductsByCategoryAsync(int id);
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductById(int id);
        Task<List<Product>> GetProductsIncludedAsync(int id);
        Task<List<OnSale_1Component>> GetOnSaleComponentsAsync();
        Task<List<OnSale_2Component>> GetOnSale_2ComponentsAsync();
        Task<List<Blog>> GetBlogsAsync();
        Task<List<WishlistProduct>> GetWishlistProductsAsync(ClaimsPrincipal user);
        Task<List<BasketProduct>> GetBasketProductsAsync(ClaimsPrincipal user);
    }
}
