using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Users.Blog
{
    public class BlogIndexVM
    {
        public BlogIndexVM()
        {
            Blogs = new List<Common.Entities.Blog>();
        }

        public List<Common.Entities.Blog> Blogs { get; set; }
    }
}
