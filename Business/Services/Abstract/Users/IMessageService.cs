using Business.ViewModels.Admin.Slider;
using Business.ViewModels.Users.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Users
{
    public interface IMessageService 
    {
        Task<bool> CreateAsync(MessageVM model, ClaimsPrincipal user);
    }
}
