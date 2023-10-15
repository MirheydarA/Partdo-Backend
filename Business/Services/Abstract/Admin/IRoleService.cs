using Business.ViewModels.Admin.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IRoleService
    {
        public Task<RoleIndexVM> IndexGetAllAsync();
        public Task<bool> CreateAsync(RoleCreateVM model);
        public Task<RoleUpdateVM> GetUpdateAsync(string id);
        public Task<bool> PostUpdateAsync(string id, RoleUpdateVM model);
        public Task<bool> DeleteAsync(string id);
    }
}
