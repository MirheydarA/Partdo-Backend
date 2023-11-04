using Business.Services.Abstract.Users;
using Business.ViewModels.Users.Message;
using Common.Entities;
using DataAccess.Repositories.Abstract.Users;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Users
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageRepsository _messageRepsository;
        private readonly UserManager<User> _userManager;
        private ModelStateDictionary _modelState;
        public MessageService(IActionContextAccessor actionContextAccessor, IUnitOfWork unitOfWork, IMessageRepsository messageRepsository, UserManager<User> userManager)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _unitOfWork = unitOfWork;
            _messageRepsository = messageRepsository;
            _userManager = userManager;
        }

        public async Task<bool> CreateAsync(MessageVM model, ClaimsPrincipal user)
        {
            var authUser = await _userManager.GetUserAsync(user);
            if (authUser is null)
            {
                return false;
            }


            if (!_modelState.IsValid) return false;

            var message = new Message
            {
                Content = model.Content,
                CreatedAt = DateTime.UtcNow,
                SenderGmail = model.SenderGmail,
                SenderName = model.SenderName,
                Subject = model.Subject,
            };

            await _messageRepsository.CreateAsync(message);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
