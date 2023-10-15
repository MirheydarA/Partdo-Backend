using Common.Constants;
using Common.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DbInitializer
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager,
                                           UserManager<User> userManager)
        {
            await SeedRolesAsync(roleManager);
            await SeedUserAsync(userManager);
        }

        private static async Task SeedUserAsync(UserManager<User> userManager)
        {
            var user = await userManager.FindByNameAsync("Admin");

            if (user == null)
            {
                user = new User
                {
                    UserName = "Admin",
                    FullName = "Admin",
                    Email = "admin12@gmail.com"
                };

                var result = await userManager.CreateAsync(user, "admin1212");

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        throw new Exception(error.Description);
                    }
                }

                await userManager.AddToRoleAsync(user, Roles.Superadmin.ToString());
            }
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Enum.GetValues<Roles>())
            {
                if (!await roleManager.RoleExistsAsync(role.ToString()))
                {
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString(),
                    });
                }
            }
        }


    }
}
