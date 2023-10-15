using Business.ViewModels.Admin.Blog;
using Business.ViewModels.Admin.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IBlogService
    {
        Task<BlogIndexVM> IndexGetAllAsync();
        //Task<BlogCreateVM> GetCreateAsync();
        Task<bool> PostCreateAsync(BlogCreateVM model);
        Task<BlogUpdateVM?> GetUpdateAsync(int id);
        Task<bool> PostUpdateAsync(int id, BlogUpdateVM model);
        Task<bool> DeleteAsync(int id);
        Task<BlogDetailsVM> DetailsAsync(int id);
    }
}
