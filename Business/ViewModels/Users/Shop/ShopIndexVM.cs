using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Business.ViewModels.Users.Shop
{
    public class ShopIndexVM : PaginationVM
    {
        public ShopIndexVM()
        {
            Categories = new List<Category>();
        }

        public List<Category> Categories { get; set; }
        public IPagedList<Product> Products { get; set; }




        #region Filter Properties
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? IsStock { get; set; }

        #endregion
    }
}
