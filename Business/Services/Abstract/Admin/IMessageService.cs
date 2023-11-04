using Business.ViewModels.Admin.Message;
using Business.ViewModels.Users.Message;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract.Admin
{
    public interface IMessageService
    {
        Task<MessageIndexVM> GetAllMessages();
    }
}
