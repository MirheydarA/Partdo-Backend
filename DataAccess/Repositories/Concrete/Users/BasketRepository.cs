﻿using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract.Users;
using DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete.Users
{
    public class BasketRepository : Repository<Basket>, IBasketRepository
    {
        private readonly AppDbContext _context;

        public BasketRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Basket> GetBasketById(User user)
        {
            var basket = await _context.Baskets.Where(b => !b.IsDeleted).FirstOrDefaultAsync(b => b.UserId == user.Id);
            return basket;
        }

        public async Task<Basket> GetBasketWithProductsAsync(User user)
        {
            var basket = await _context.Baskets.Include(b => b.BasketProducts).ThenInclude(bp => bp.Product).Where(b => !b.IsDeleted).FirstOrDefaultAsync(b => b.UserId == user.Id);
            return basket;
        }

        public async Task<BasketProduct>? GetProductByBasketProductIdAsync(int id, User user)
        {
            var basketProduct = await _context.BasketProducts.Where(bp => !bp.IsDeleted).FirstOrDefaultAsync(bp => bp.ProductId == id && bp.Basket.UserId == user.Id);
            return basketProduct;
        }
    }
}
