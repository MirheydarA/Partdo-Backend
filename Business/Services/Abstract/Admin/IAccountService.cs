using Business.ViewModels.Admin.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IAccountService
    {
        Task<bool> LoginAsync(AccountLoginVM model);
        Task<bool> Logout();
    }
}
