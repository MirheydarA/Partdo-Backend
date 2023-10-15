using Common.Entities;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstract.Admin
{
    public interface ISubCategoryRepository : IRepository<SubCategory>
    {
        Task<List<SubCategory>> GetSubCategoriesWithCategory();
        Task<SubCategory> GetByIdWithInclude(int id);
        Task<bool> GetByName(string name);

    }
}
