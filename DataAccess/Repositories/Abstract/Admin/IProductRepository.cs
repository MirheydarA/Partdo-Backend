﻿using Common.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract.Admin
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetProductsWithIncludedAsync();
        Task<List<Product>> GetAllProductsWithIncludedAsync();
        Task<bool> GetProductByNameAsync(string name);
        Task<Product> GetProductWithIncludeById(int id);
        Task<List<Product>> GetProductWithIncludeByCategoryId(int id);
        Task<List<Product>> FilterByIdAsync(int id);
        Task<List<Product>> GetProductWithIncludeByCategoryIdTake5(int id);
        Task<Product> GetByIdCustom(int id);
        IQueryable<Product> FilterByName(string? name);
        IQueryable<Product> FilterByCategory(IQueryable<Product> products, List<int>? id);
        IQueryable<Product> FilterByPrice(IQueryable<Product> products, decimal? minPrice, decimal? maxPrice);
        IQueryable<Product> FilterByStockType(string? stock);
        IQueryable<Product> FilteredByTiresWheels(string? categoryName);
    }
}
