using Business.ViewModels.Admin.Category;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface ICategoryService
    {
        Task<CategoryIndexVM> GetAllAsync();
        Task<bool> CreateAsync(CategoryCreateVM model);
        Task<CategoryUpdateVM?> GetUpdateAsync(int id);
        Task<bool> PostUpdateAsync(int id, CategoryUpdateVM model);
        Task<bool> DeleteAsync(int id);
    }
}
