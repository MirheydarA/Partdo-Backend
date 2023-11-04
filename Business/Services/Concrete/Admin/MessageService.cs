using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.Message;
using DataAccess.Repositories.Abstract.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Admin
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepsository _messageRepsository;

        public MessageService(IMessageRepsository messageRepsository)
        {
            _messageRepsository = messageRepsository;
        }

        public async Task<MessageIndexVM> GetAllMessages()
        {
            var model = new MessageIndexVM();
            model.Messages = await _messageRepsository.GetAllAsync();

            return model;
        }
    }
}
