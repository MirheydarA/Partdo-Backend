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
    public class WishlistProductRepository : Repository<WishlistProduct>, IWishlistProductRepository
    {
        private readonly AppDbContext _context;

        public WishlistProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<WishlistProduct> GetWishlistProductByIdAsync(int id, Wishlist wishlist)
        {
            var wishlistProduct = await _context.WishlistProducts.Where(wp => !wp.IsDeleted).FirstOrDefaultAsync(wp => wp.Id == id && wp.BasketId == wishlist.Id);
            return wishlistProduct;
        }

        public Task<WishlistProduct>? GetProductByWishlistProductIdAsync(int id, User user)
        {
            var wishlistproduct = _context.WishlistProducts.Where(wp => !wp.IsDeleted).FirstOrDefaultAsync(wp => wp.ProductId == id && wp.Wishlist.UserId == user.Id);
            return wishlistproduct;
        }


    }
}
