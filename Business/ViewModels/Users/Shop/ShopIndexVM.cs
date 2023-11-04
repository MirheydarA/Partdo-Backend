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
        public List<Category> Categories { get; set; }
        public IPagedList<Product> Products { get; set; }
        public List<WishlistProduct> WishlistProducts { get; set; }
        public List<BasketProduct> BasketProducts { get; set; }

        #region Filter Properties
        public string? Name { get; set; }
        public string? CategoryName { get; set; }
        public List<int>? CategoryIds { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? IsStock { get; set; }

        #endregion
        public ShopIndexVM()
        {
            Categories = new List<Category>();
            CategoryIds = new List<int>();
        }
    }
}
