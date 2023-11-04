using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract.Users;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete.Users
{
    public class MessageRepository : Repository<Message>, IMessageRepsository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {
        }
    }
}
