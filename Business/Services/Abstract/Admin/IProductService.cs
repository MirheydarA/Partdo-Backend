using Business.ViewModels.Admin.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IProductService
    {
        Task<ProductIndexVM> IndexGetAllAsync(ProductIndexVM model);
        Task<ProductCreateVM> GetCreateAsync();
        Task<bool> PostCreateAsync(ProductCreateVM model);
        Task<ProductUpdateVM?> GetUpdateAsync(int id);
        Task<bool> PostUpdateAsync(int id, ProductUpdateVM model);
        Task<bool> DeleteAsync(int id);
        Task<ProductDetailsVM> DetailsAsync(int id);
    }
}
