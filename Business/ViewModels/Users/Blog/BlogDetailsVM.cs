using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Users.Blog
{
    public class BlogDetailsVM
    {
        public string Title { get; set; }
        public string ShortDesc { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string Author { get; set; }
        public string Photo { get; set; }
        public string Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
