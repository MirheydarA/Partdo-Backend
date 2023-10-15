using Business.ViewModels.Admin.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface ISubCategoryService
    {
        Task<SubCategoryIndexVM> IndexGetAllAsync();
        Task<SubCategoryCreateVM> GetCreateAsync();
        Task<bool> PostCreateAsync(SubCategoryCreateVM model);
        Task<SubCategoryUpdateVM?> GetUpdateAsync(int id);
        Task<bool> PostUpdateAsync(int id, SubCategoryUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
