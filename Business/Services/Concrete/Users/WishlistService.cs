using Business.Services.Abstract.Users;
using Business.ViewModels.Users.Basket;
using Business.ViewModels.Users.Wishlist;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Abstract.Users;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Users
{
    public class WishlistService : IWishlistService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataAccess.Repositories.Abstract.Admin.IProductRepository _productRepository;
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IWishlistProductRepository _wishlistProductRepository;

        public WishlistService(UserManager<User> userManager,
                               IUnitOfWork unitOfWork,
                               DataAccess.Repositories.Abstract.Admin.IProductRepository productRepository,
                               IWishlistRepository wishlistRepository,
                               IWishlistProductRepository wishlistProductRepository)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _wishlistRepository = wishlistRepository;
            _wishlistProductRepository = wishlistProductRepository;
        }

        public async Task<List<WishlistVM>> IndexGetWishlist(ClaimsPrincipal user)
        {
            var authUser = await _userManager.GetUserAsync(user);
            if (authUser is null)
            {
                return null;
            }


            var wishlist = await _wishlistRepository.GetWishlistWithProductsAsync(authUser);

            var model = new List<WishlistVM>();

            if (wishlist is null)
            {
                return null;
            }

            foreach (var wishlistProduct in wishlist.WishlistProducts.Where(wp => !wp.IsDeleted))
            {
                var wishlistItem = new WishlistVM
                {
                    Id = wishlistProduct.Id,
                    PhotoName = wishlistProduct.Product.MainPhoto,
                    Price = wishlistProduct.Product.Price,
                    NewPrice = wishlistProduct.Product.NewPrice,
                    StockType = wishlistProduct.Product.StockType,
                    Title = wishlistProduct.Product.Name,
                };
                model.Add(wishlistItem);
            }

            return model;
        }
        public async Task<bool> AddAsync(ClaimsPrincipal user, int id)
        {
            var authUser = await _userManager.GetUserAsync(user);
            if (authUser is null)
            {
                return false;
            }

            var wishlist = await _wishlistRepository.GetWishlistById(authUser);

            if (wishlist is null)
            {
                wishlist = new Wishlist
                {
                    UserId = authUser.Id,
                };
                await _wishlistRepository.CreateAsync(wishlist);
            }

            var product = await _productRepository.GetByIdAsync(id);
            if (product is null) return false;

            var wishlistProduct = await _wishlistProductRepository.GetProductByWishlistProductIdAsync(id, authUser);

            if (wishlistProduct is null)
            {
                wishlistProduct = new WishlistProduct
                {
                    Wishlist = wishlist,
                    ProductId = product.Id,
                    Count = 1
                };

                await _wishlistProductRepository.CreateAsync(wishlistProduct);
            }

            else if (wishlistProduct.IsDeleted)
            {
                wishlistProduct.IsDeleted = false;
                wishlistProduct.Count = 1;
            }
            else
            {
                wishlistProduct.Count++;
                _wishlistProductRepository.Update(wishlistProduct);
            }

            await _unitOfWork.CommitAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(ClaimsPrincipal user, int id)
        {
            var authUser = await _userManager.GetUserAsync(user);
            if (authUser is null)
            {
                return false;
            }

            var wishlist = await _wishlistRepository.GetWishlistById(authUser);
            if (wishlist is null) return false;

            var wishlistProduct = await _wishlistProductRepository.GetWishlistProductByIdAsync(id, wishlist);
            if (wishlistProduct is null) return false;

            var product = await _productRepository.GetByIdCustom(wishlistProduct.Id);
            if (product is null) return false;

            _wishlistProductRepository.Delete(wishlistProduct);
            wishlistProduct.IsDeleted = true;
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
