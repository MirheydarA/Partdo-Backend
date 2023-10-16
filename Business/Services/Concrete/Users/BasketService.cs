using Business.Services.Abstract.Users;
using Business.ViewModels.Users.Basket;
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
    public class BasketService : IBasketService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;
        private readonly DataAccess.Repositories.Abstract.Admin.IProductRepository _productRepository;
        private readonly IBasketProductRepository _basketProductRepository;

        public BasketService(UserManager<User> userManager,
                             IUnitOfWork unitOfWork,
                             IBasketRepository basketRepository,
                             DataAccess.Repositories.Abstract.Admin.IProductRepository productRepository,
                             IBasketProductRepository basketProductRepository)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
            _productRepository = productRepository;
            _basketProductRepository = basketProductRepository;
        }
        public async Task<List<BasketVM>> IndexGetBasket(ClaimsPrincipal user)
        {
            var authUser = await _userManager.GetUserAsync(user);
            if (authUser == null)
            {
                return null;
            }

            var basket = await _basketRepository.GetBasketWithProductsAsync(authUser);

            var model = new List<BasketVM>();

            if (basket is null)
            {
                return null;
            }

            foreach (var basketProduct in basket.BasketProducts)
            {
                var basketItem = new BasketVM
                {
                    Id = basketProduct.Id,
                    Count = basketProduct.Count,
                    PhotoName = basketProduct.Product.MainPhoto,
                    StockQuantity = basketProduct.Product.StockQuantity,
                    Title = basketProduct.Product.Name,
                    Price = basketProduct.Product.Price,
                };
                model.Add(basketItem);
            }

            return model;
        }
        public async Task<bool> AddAsync(ClaimsPrincipal user, int id)
        {
            var authUser = await _userManager.GetUserAsync(user);
            if (authUser == null)
            {
                return false;
            }

            var basket = await _basketRepository.GetBasketById(authUser);

            if (basket is null)
            {
                basket = new Basket
                {
                    UserId = authUser.Id,
                };
                await _basketRepository.CreateAsync(basket);
            }

            var product = await _productRepository.GetByIdAsync(id);
            if (product is null) return false;

            var basketProduct = await _basketRepository.GetProductByBasketProductIdAsync(id, authUser);

            if (basketProduct is null)
            {
                basketProduct = new BasketProduct
                {
                    Basket = basket,
                    ProductId = product.Id,
                    Count = 1
                };
                await _basketProductRepository.CreateAsync(basketProduct);
            }

            else
            {
                basketProduct.Count ++;
                _basketProductRepository.Update(basketProduct);
            }

            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> IncreaseAsync(ClaimsPrincipal user, int id)
        {
            var authUser = await _userManager.GetUserAsync(user);
            if (authUser == null)
            {
                return false;
            }

            var basket = await _basketRepository.GetBasketById(authUser);
            if (basket == null) return false;

            var basketProduct = await _basketProductRepository.GetBasketProductByIdAsync(id, basket);
            if (basketProduct == null)
            {
                return false;
            }
            var product = await _productRepository.GetByIdAsync(basketProduct.ProductId);
            if (product == null)
            {
                return false;
            }

            if (product.StockQuantity == basketProduct.Count)
                return false;

            basketProduct.Count++;

            _basketProductRepository.Update(basketProduct);
            await _unitOfWork.CommitAsync();

            return true;

        }
        public async Task<bool> DecreaseAsync(ClaimsPrincipal user, int id)
        {
            var authUser = await _userManager.GetUserAsync(user);
            if (authUser == null)
            {
                return false;
            }

            var basket = await _basketRepository.GetBasketById(authUser);
            if (basket == null) return false;

            var basketProduct = await _basketProductRepository.GetBasketProductByIdAsync(id, basket);
            if (basketProduct == null)
            {
                return false;
            }

            if (basketProduct.Count == 0)
                return false;

            basketProduct.Count--;

            _basketProductRepository.Update(basketProduct);
            await _unitOfWork.CommitAsync();

            return true;
        }
        public async Task<bool> DeleteAsync(ClaimsPrincipal user, int id)
        {
            var authUser = await _userManager.GetUserAsync(user);
            if (authUser == null)
            {
                return false;
            }

            var basket = await _basketRepository.GetBasketById(authUser);
            if (basket == null) return false;

            var basketProduct = await _basketProductRepository.GetBasketProductByIdAsync(id, basket);
            if (basketProduct == null)
            {
                return false;
            }

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }

            _basketProductRepository.Delete(basketProduct);
            await _unitOfWork.CommitAsync();
            return true;
        }


    }
}
