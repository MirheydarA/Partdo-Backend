using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete.Admin
{
    public class SubCategoryRepository : Repository<SubCategory>, ISubCategoryRepository
    {
        private readonly AppDbContext _context;

        public SubCategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SubCategory> GetByIdWithInclude(int id)
        {
            var subCategory = await _context.SubCategories.Where(s => !s.IsDeleted).Include(s =>s.Category).FirstOrDefaultAsync(c => c.Id == id);
            return subCategory;
        }

        public async Task<bool> GetByName(string name)
        {
            var subCategory = await _context.SubCategories.Where(s => !s.IsDeleted).FirstOrDefaultAsync(s => s.Name == name);
            if (subCategory is null)
            {
                return true;
            }
            return false;
        }

        public async Task<List<SubCategory>> GetSubCategoriesWithCategory()
        {
            var subCategories = await _context.SubCategories.Include(s => s.Category).Where(s => !s.IsDeleted).OrderByDescending(s => s.Id).ToListAsync();
            return subCategories;
        }
    }
}
