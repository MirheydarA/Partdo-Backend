﻿using Business.Services.Abstract.Users;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Users.Shop;
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
    public class ShopService : IShopService
    {
        private readonly DataAccess.Repositories.Abstract.Admin.IProductRepository _productRepository;
        private readonly IPaginator _paginator;
        private readonly DataAccess.Repositories.Abstract.Users.ICategoryRepository _categoryRepository;

        public ShopService(DataAccess.Repositories.Abstract.Admin.IProductRepository productRepository,
                           IPaginator paginator,
                           DataAccess.Repositories.Abstract.Users.ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _paginator = paginator;
            _categoryRepository = categoryRepository;
        }

        public async Task<ShopIndexVM> IndexGetAllAsync(ShopIndexVM model)
        {


            //var products = await _productRepository.GetAllAsync();

            var products = _productRepository.FilterByName(model.Name);

            products = _productRepository.FilteredByTiresWheels(model.CategoryName);
            products = _productRepository.FilterByCategory(products, model.CategoryIds);
            products = _productRepository.FilterByPrice(products, model.MinPrice, model.MaxPrice);
            //products = _productRepository.FilterByStockType();

            var productList = await products.ToListAsync();

            model = new ShopIndexVM
            {
                Products = _paginator.GetPagedList(productList, model.CurrentPage, model.PageSize),
                Categories = await _categoryRepository.GetAllAsync(),
                CategoryIds = model.CategoryIds,
            };

            return model;
        }

        public async Task<ShopDetailsVM>? DetailsAsync(int id)
        {
            var product = await _productRepository.GetProductWithIncludeById(id);
            if (product is null) return null;

            var includedProduct = new ShopDetailsVM
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                MainPhoto = product.MainPhoto,
                NewPrice = product.NewPrice,
                Photos = product.Photos,
                Price = product.Price,
                ShortDesc = product.ShortDesc,
                Status = product.Status,
                StockQuantity = product.StockQuantity,
                StockType = product.StockType,
                Category = product.SubCategory.Category.Name
            };

            return includedProduct;
        }
    }
}
