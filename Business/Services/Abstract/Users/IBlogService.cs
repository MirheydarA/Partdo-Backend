using Business.ViewModels.Users.Blog;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Users
{
    public interface IBlogService
    {
        Task<BlogIndexVM> IndexGetAllAsync();
        Task<BlogDetailsVM> DetailsAsync(int id);
    }
}
