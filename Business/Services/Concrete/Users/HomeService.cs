﻿using Business.Services.Abstract.Users;
using Business.ViewModels.Users.Home;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Abstract.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public HomeService(ISliderRepository sliderRepository,
                           DataAccess.Repositories.Abstract.Users.ICategoryRepository categoryRepository,
                           DataAccess.Repositories.Abstract.Admin.IProductRepository productRepository,
                           IOnSale_1Repository onSale_1Repository,
                           IOnSale_2Repository onSale_2Repository,
                           IBlogRepository blogRepository)
        {
            _sliderRepository = sliderRepository;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _onSale_1Repository = onSale_1Repository;
            _onSale_2Repository = onSale_2Repository;
            _blogRepository = blogRepository;
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

    }
}