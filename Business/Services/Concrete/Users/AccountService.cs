using Business.Services.Abstract.Users;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Users.Account;
using Common.Constants;
using Common.Entities;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Users
{
    public class AccountService : IAccountService
    {
        private ModelStateDictionary _modelState;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;

        public AccountService(IActionContextAccessor actionContextAccessor,
                              IUnitOfWork unitOfWork,
                              UserManager<Common.Entities.User> userManager,
                              SignInManager<Common.Entities.User> signInManager,
                              RoleManager<IdentityRole> roleManager,
                              IEmailSender emailSender)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }
        public async Task<bool> RegisterAsync(AccountRegisterVM model)
        {
            if (!_modelState.IsValid) return false;

            var user = new User
            {
                Email = model.Email,
                UserName = model.Username, 
            };

            string emailpattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regexemail = new Regex(emailpattern);
            if (!regexemail.IsMatch(user.Email))
            {
                _modelState.AddModelError("Email", "Email is not correct format");
                return false;
            }

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _modelState.AddModelError("Password", error.Description);
                }
                return false;
            }


            await _userManager.AddToRoleAsync(user, Roles.User.ToString());
            return true;
        }
        public async Task<bool> LoginAsync(AccountLoginVM model)
        {
            if (!_modelState.IsValid) return false;

            var user = await _userManager.FindByNameAsync(model.Email) ?? await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
                _modelState.AddModelError(string.Empty, "Email or Password is incorrect");
                return false;
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
            {
                _modelState.AddModelError(string.Empty, "Email or Password is incorrect");
                return false;
            }

            return true;
        }

        public async Task<bool> Logout()
        {
            await _signInManager.SignOutAsync();
            return true;
        }

    }
}
