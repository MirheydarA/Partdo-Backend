using Business.ViewModels.Admin.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IUserService
    {
        public Task<UserIndexVM> IndexGetAllAsync();
        public Task<UserCreateVM> GetCreateAsync();
        public Task<bool> PostCreateAsync(UserCreateVM model);
        public Task<UserUpdateVM?> GetUpdateAsync(string id);
        public Task<bool> PostUpdateAsync(string id, UserUpdateVM model);
        public Task<bool> DeleteAsync(string id);
        public Task<UserDetailsVM> DetailsAsync(string id);
    }
}
