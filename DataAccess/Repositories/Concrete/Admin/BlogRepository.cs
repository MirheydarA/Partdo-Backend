using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete.Admin
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        private readonly AppDbContext _context;

        public BlogRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> GetBlogByTitle(string title)
        {
            var blog = await _context.Blogs.Where(b => !b.IsDeleted).FirstOrDefaultAsync(b => b.Title.ToLower().Trim() == title.ToLower().Trim());
            if (blog is null)
            {
                return true;
            }
            return false;
        }

    }
}
