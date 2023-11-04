using Business.ViewModels.Users.Shop;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Users
{
    public interface IShopService
    {
        Task<ShopIndexVM> IndexGetAllAsync(ShopIndexVM model);
        Task<ShopDetailsVM> DetailsAsync(int id);
    }
}
