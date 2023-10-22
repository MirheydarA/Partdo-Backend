﻿using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete.Admin
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> GetProductByNameAsync(string name)
        {
            var product = await _context.Products.Where(p => !p.IsDeleted).FirstOrDefaultAsync(p => p.Name == name);
            if (product is null)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Product>> GetProductsWithIncludedAsync()
        {
            var products = await _context.Products.Where(p => !p.IsDeleted).Include(p => p.Photos).Include(p => p.SubCategory).ThenInclude(s => s.Category).OrderByDescending(p => p.Id).ToListAsync();
            return products;
        }

        public async Task<List<Product>> GetAllProductsWithIncludedAsync()
        {
            var products = await _context.Products.Include(p => p.Photos).Include(p => p.SubCategory).ThenInclude(s => s.Category).OrderByDescending(p => p.Id).ToListAsync();
            return products;
        }

        public Task<Product> GetProductWithIncludeById(int id)
        {
            var product = _context.Products.Where(p => !p.IsDeleted).Include(p => p.Photos).Include(p => p.SubCategory).ThenInclude(s => s.Category).FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public Task<List<Product>> FilterByIdAsync(int id)
        {
            var products = _context.Products.Where(p => !p.IsDeleted).Include(p => p.Photos).Include(p => p.SubCategory).ThenInclude(s => s.Category).Where(p => p.SubCategory.CategoryId == id).ToListAsync();
            return products;
        }

        public async Task<Product> GetByIdCustom(int id)
        {
            var product = await _context.Products.Where(p => !p.IsDeleted).FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public IQueryable<Product> FilterByName(string? name)
        {
            var products = !string.IsNullOrEmpty(name) ? _context.Products.Include(p => p.SubCategory).ThenInclude(s => s.Category).Where(p => p.Name.Contains(name)) : _context.Products.Include(p => p.SubCategory).ThenInclude(s => s.Category).Where(p => !p.IsDeleted);
            return products;
        }

        public IQueryable<Product> FilterByCategory(int? id)
        {
            var products = id != null ? _context.Products.Include(p => p.SubCategory).ThenInclude(s => s.Category).Where(p => p.SubCategory.CategoryId == id) : _context.Products.Include(p => p.SubCategory).ThenInclude(s => s.Category).Where(p => !p.IsDeleted);
            return products;
        }

        public IQueryable<Product> FilterByPrice(IQueryable<Product> products, decimal? minPrice, decimal? maxPrice)
        {
            return products.Where(p => minPrice != null ? p.Price >= minPrice || p.NewPrice >= minPrice : true && maxPrice != null ? p.Price <= maxPrice : true);
        }

        public IQueryable<Product> FilterByStockType(string? stock)
        {
            var products = stock != null ? _context.Products.Include(p => p.SubCategory).ThenInclude(s => s.Category).Where(p => p.StockType.ToString() == stock) : _context.Products.Include(p => p.SubCategory).ThenInclude(s => s.Category).Where(p => !p.IsDeleted);
            return products;
        }

    }
}
