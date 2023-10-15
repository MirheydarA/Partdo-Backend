using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Role
{
    public class RoleIndexVM
    {
        public RoleIndexVM()
        {
            Roles = new List<IdentityRole>();
        }
        public List<IdentityRole> Roles { get; set; }
    }
}
