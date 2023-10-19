using Business.Services.Concrete.Users;
using Business.ViewModels.Users.Basket;
using Business.ViewModels.Users.Wishlist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Users
{
    public interface IWishlistService
    {
        Task<List<WishlistVM>> IndexGetWishlist(ClaimsPrincipal user);
        Task<bool> AddAsync(ClaimsPrincipal user, int id);
        Task<bool> DeleteAsync(ClaimsPrincipal user, int id);
    }
}
