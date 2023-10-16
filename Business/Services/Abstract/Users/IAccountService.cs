using Business.ViewModels.Users.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Users
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(AccountRegisterVM model);
        Task<bool> LoginAsync(AccountLoginVM model);
        Task<bool> Logout();

    }
}
