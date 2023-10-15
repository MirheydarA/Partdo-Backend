using Business.ViewModels.Admin.Blog;
using Business.ViewModels.Admin.OnSale_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IOnSale_1Service
    {
        Task<OnSale_1IndexVM> IndexGetAllAsync();
        Task<bool> PostCreateAsync(OnSale_1CreateVM model);
        Task<OnSale_1UpdateVM?> GetUpdateAsync(int id);
        Task<bool> PostUpdateAsync(int id, OnSale_1UpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
