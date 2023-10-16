using Common.Entities;
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

        public Task<Basket> GetBasketById(User user)
        {
            var basket = _context.Baskets.Where(b => !b.IsDeleted).FirstOrDefaultAsync(b => b.UserId == user.Id);
            return basket;
        }

        public Task<Basket> GetBasketWithProductsAsync(User user)
        {
            var basket = _context.Baskets.Include(b => b.BasketProducts).ThenInclude(bp => bp.Product).Where(b => !b.IsDeleted).FirstOrDefaultAsync(b => b.UserId == user.Id);
            return basket;
        }

        public Task<BasketProduct>? GetProductByBasketProductIdAsync(int id, User user)
        {
            var basketProduct = _context.BasketProducts.FirstOrDefaultAsync(bp => bp.ProductId == id && bp.Basket.UserId == user.Id);
            return basketProduct;
        }
    }
}
