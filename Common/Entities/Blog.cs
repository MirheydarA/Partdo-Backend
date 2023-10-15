using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string ShortDesc { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string Author { get; set; }
        public string Photo { get; set; }
        public string Category { get; set; }
    }
}
