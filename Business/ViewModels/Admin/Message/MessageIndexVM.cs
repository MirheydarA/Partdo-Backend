using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Message
{
    public class MessageIndexVM
    {
        public MessageIndexVM()
        {
            Messages = new List<Common.Entities.Message>();
        }

        public List<Common.Entities.Message> Messages { get; set; }
    }
}
