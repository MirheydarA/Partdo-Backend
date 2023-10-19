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
    public class WishlistRepository : Repository<Wishlist>, IWishlistRepository
    {
        private readonly AppDbContext _context;

        public WishlistRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<Wishlist> GetWishlistById(User user)
        {
            var wishlist = _context.Wishlists.Where(w => !w.IsDeleted).FirstOrDefaultAsync(w => w.UserId == user.Id);
            return wishlist;
        }

        public async Task<Wishlist> GetWishlistWithProductsAsync(User user)
        {
            var wishlist = await _context.Wishlists.Include(w => w.WishlistProducts).ThenInclude(wp => wp.Product).Where(w => !w.IsDeleted).FirstOrDefaultAsync(w => w.UserId != user.Id);
            return wishlist;
        }
    }
}
