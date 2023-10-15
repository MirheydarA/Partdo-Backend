using Common.Constants;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete.Admin
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<bool> HasAccessToAdminPanelAsync(User user)
        {

            var roles =  _roleManager.Roles;
            var userRoles =await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                if(roles.Any(x=> x.Name == role))
                {
                    return true;
                }
            }
          
            return false;


            //if (await _userManager.IsInRoleAsync(user, Roles.Admin.ToString()) ||
            //    await _userManager.IsInRoleAsync(user, Roles.Superadmin.ToString()) ||
            //    await _userManager.IsInRoleAsync(user, Roles.HR.ToString()))
            //{
            //    return true;
            //}

            //return false;
        }
    }
}
