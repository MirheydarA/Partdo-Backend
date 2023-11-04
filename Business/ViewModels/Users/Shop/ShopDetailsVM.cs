using Common.Entities;
using DataAccess.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Users.Shop
{
    public class ShopDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDesc { get; set; }
        [EnumDataType(typeof(ProductStock))]
        public ProductStock StockType { get; set; }
        public int StockQuantity { get; set; }
        public string MainPhoto { get; set; }
        public ICollection<ProductPhoto> Photos { get; set; }
        public string? Status { get; set; }
        public decimal Price { get; set; }
        public decimal? NewPrice { get; set; }
        public string Category { get; set; }
        //public int SubCategoryId { get; set; }
        //public SubCategory SubCategory { get; set; }
        //public bool IsSpecial { get; set; }
        //public bool IsMain { get; set; }
    }
}
