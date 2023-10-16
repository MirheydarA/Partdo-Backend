using Common.Entities;
using DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract.Users
{
    public interface IBasketProductRepository : IRepository<BasketProduct>
    {
        Task<BasketProduct> GetBasketProductByIdAsync(int id, Basket basket);
    }
}
