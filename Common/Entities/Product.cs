using Common.Entities.Base;
using DataAccess.Constants;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDesc { get; set; }
        [EnumDataType(typeof(ProductStock))]
        public ProductStock StockType  { get; set; }
        public int StockQuantity { get; set; }
        public string MainPhoto { get; set; }
        public ICollection<ProductPhoto> Photos { get; set; }
        public string? Status { get; set; }
        public decimal Price { get; set; }
        public decimal? NewPrice { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsMain { get; set; }
    }
}
