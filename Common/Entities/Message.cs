using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Message : BaseEntity
    {
        public string SenderName { get; set; }
        public string SenderGmail { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
