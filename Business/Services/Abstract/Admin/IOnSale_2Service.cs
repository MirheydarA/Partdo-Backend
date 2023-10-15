using Business.ViewModels.Admin.OnSale_1;
using Business.ViewModels.Admin.OnSale_2;
using Common.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IOnSale_2Service 
    {
        Task<OnSale_2IndexVM> IndexGetAllAsync();
        Task<bool> PostCreateAsync(OnSale_2CreateVM model);
        Task<OnSale_2UpdateVM?> GetUpdateAsync(int id);
        Task<bool> PostUpdateAsync(int id, OnSale_2UpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
