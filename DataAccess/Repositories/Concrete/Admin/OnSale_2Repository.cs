using Common.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete.Admin
{
    public class OnSale_2Repository : Repository<OnSale_2Component>, IOnSale_2Repository
    {
        public OnSale_2Repository(AppDbContext context) : base(context)
        {
        }
    }
}
