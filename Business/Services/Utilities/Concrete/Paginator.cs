using Business.Services.Utilities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Business.Services.Utilities.Concrete
{
    public class Paginator : IPaginator
    {
        public IPagedList<T> GetPagedList<T>(ICollection<T> listUnpaged, int page = 1, int pageSize = 1)
        {
            var listPaged = listUnpaged.ToPagedList(page, pageSize);

            if (listPaged.PageNumber != 1 && page > listPaged.PageCount)
                return null;

            return listPaged;
        }
    }
}
