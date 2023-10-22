using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Business.Services.Utilities.Abstract
{
    public interface IPaginator
    {
        public IPagedList<T> GetPagedList<T>(ICollection<T> listUnpaged, int currentPage = 1, int pageSize = 1);
    }
}
