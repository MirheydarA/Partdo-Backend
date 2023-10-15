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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Product> FilterByCategory(int? id)
        {
            var products = _context.Products.Include(p => p.SubCategory).ThenInclude(p => p.Category).Where(p => p.SubCategory.CategoryId == id);
            return products;
        }
    }
}
