using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.User;
using Common.Constants;
using Common.Entities;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Admin
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private ModelStateDictionary _modelState;

        public UserService(UserManager<User> userManager, 
                           RoleManager<IdentityRole> roleManager,
                           IUnitOfWork unitOfWork,
                           IActionContextAccessor actionContextAccessor) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<UserIndexVM> IndexGetAllAsync()
        {
            var model = new UserIndexVM();
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (!await _userManager.IsInRoleAsync(user, Roles.Superadmin.ToString()))
                {
                    var userWithRoles = new UserVM
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Fullname = user.FullName,
                        Username = user.UserName,
                        Roles = roles.ToList(),
                    };

                    model.Users.Add(userWithRoles);
                }
            }

            return model;
        }
        public async Task<UserCreateVM> GetCreateAsync()
        {
            var model = new UserCreateVM
            {
                Roles = await _roleManager.Roles.Where(r => r.Name != Roles.User.ToString() && r.Name != Roles.Superadmin.ToString()).Select(r => new SelectListItem
                {
                    Value = r.Id,
                    Text = r.Name
                }).ToListAsync()
            };

            return model;
        }

        public async Task<bool> PostCreateAsync(UserCreateVM model)
        {
            model.Roles = await _roleManager.Roles.Where(r => r.Name != Roles.User.ToString() &&
                                                              r.Name != Roles.Superadmin.ToString())
                                                  .Select(r => new SelectListItem
                                                  {
                                                      Value = r.Id,
                                                      Text = r.Name
                                                  })
                                                   .ToListAsync();

            //if (model.RolesIds is null)
            //{
            //    _modelState.AddModelError("Fullname", "Role empty");
            //    return false;
            //}

            if (!_modelState.IsValid) return false;

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                _modelState.AddModelError("Username", "This Username already used");
                return false;
            }

            user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FullName = model.Fullname
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            //if (!result.Succeeded)
            //{
            //    foreach (var error in result.Errors)
            //    {
            //        _modelState.AddModelError(string.Empty, error.Description);
            //    }
            //    return false;
            //}


            foreach (var roleId in model.RolesIds)
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role == null)
                {
                    _modelState.AddModelError("RolesIds", "Role does not exist");
                    return false;
                }

                result = await _userManager.AddToRoleAsync(user, role.Name);
                //if (!result.Succeeded)
                //{
                //    foreach (var error in result.Errors)
                //        _modelState.AddModelError(string.Empty, error.Description);

                //    return false;
                //}
            }
            return true;
        }
        public async Task<UserUpdateVM?> GetUpdateAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null) return null;

            var roles = await _userManager.GetRolesAsync(user);


            var roleIds = new List<string>();
            foreach (var roleName in roles)
            {
                var role =  await _roleManager.FindByNameAsync(roleName);
                if (role is null) return null;

                roleIds.Add(role.Id);
            }

            var model = new UserUpdateVM()
            {
                Fullname = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Username = user.UserName,

                Roles = await _roleManager.Roles.Where(r => r.Name != Roles.User.ToString() && r.Name != Roles.Superadmin.ToString()).Select(r => new SelectListItem
                {
                    Value = r.Id,
                    Text = r.Name
                }).ToListAsync(),

                RolesIds = roleIds
            };

            return model;
        }
        public async Task<bool> PostUpdateAsync(string id, UserUpdateVM model)
        {
            model.Roles = await _roleManager.Roles.Where(r => r.Name != Roles.User.ToString() &&
                                                             r.Name != Roles.Superadmin.ToString())
                                                 .Select(r => new SelectListItem
                                                 {
                                                     Value = r.Id,
                                                     Text = r.Name
                                                 })
                                                  .ToListAsync();

            if (!_modelState.IsValid) return false;
            
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            user.FullName = model.Fullname; 
            user.Email = model.Email; 
            user.PhoneNumber = model.PhoneNumber;
            user.UserName = model.Username;

            if (model.Password != null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _modelState.AddModelError(string.Empty, error.Description);
                }
                return false;
            }

            var userRoles = new List<IdentityRole>();
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var roleName in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role == null) return false;

                userRoles.Add(role);
            }

            var removedIds = userRoles.FindAll(x => !model.RolesIds.Contains(x.Id)).ToList();


            foreach (var removedRole in removedIds)
            {
                await _userManager.RemoveFromRoleAsync(user, removedRole.Name);
            }

            foreach (var roleId in model.RolesIds)
            {
                var role = await _roleManager.FindByIdAsync(roleId);

                if (role is null)
                {
                    _modelState.AddModelError("RolesIds", "Role does not exist");
                    return false;
                }

                if (!await _userManager.IsInRoleAsync(user, role.Name))
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }

                else if(!userRoles.Any(r => r.Id == role.Id))
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return false;

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    throw new Exception(error.Description);
                }
            }
            return true;
        }

        public async Task<UserDetailsVM> DetailsAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null) return null;

            var roles = await _userManager.GetRolesAsync(user);

            var userWithRoles = new UserDetailsVM
            {
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Roles = roles.ToList(),
            };

            return userWithRoles;
        }
    }
}
