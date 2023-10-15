using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract.Users;
using DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete.Users
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesWithSubCategoryAsync()
        {
            var categories = await _context.Categories.Include(c => c.SubCategories).Where(c => !c.IsDeleted).Take(3).ToListAsync();
            return categories;
        }
    }
}
