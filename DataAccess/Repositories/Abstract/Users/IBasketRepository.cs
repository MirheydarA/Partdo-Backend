using Common.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract.Users
{
    public interface IBasketRepository : IRepository<Basket>
    {
        Task<Basket> GetBasketWithProductsAsync(User user);
        Task<Basket> GetBasketById(User user);
        Task<BasketProduct>? GetProductByBasketProductIdAsync(int id, User user);
    }
}
