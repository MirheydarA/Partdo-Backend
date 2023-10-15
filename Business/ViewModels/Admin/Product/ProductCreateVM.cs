using DataAccess.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Product
{
    public class ProductCreateVM
    {
        [Required, MinLength(3, ErrorMessage = "Name must be at least 3 charachter")]
        public string Name { get; set; }
        [Required, MinLength(3, ErrorMessage = "Description must be at least 3 charachter")]
        public string Description { get; set; }
        [Required, MinLength(3, ErrorMessage = "Shortdesc must be at least 3 charachter")]
        public string ShortDesc { get; set; }
        [EnumDataType(typeof(ProductStock))]
        public ProductStock StockType { get; set; }
        [Required]
        public int StockQuantity { get; set; }
        [Required]
        public IFormFile MainPhoto { get; set; }
        [Required]
        public List<IFormFile> Photos { get; set; }
        public string? Status { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal? NewPrice { get; set; }
        [Required]
        [Display(Name = "SubCategory")]
        public int SubCategroyId { get; set; }
        public List<SelectListItem>? SubCategories { get; set; }
        [Display(Name = "Special Product")]
        public bool IsSpecial { get; set; }
        [Display(Name = "Main Product")]
        public bool IsMain { get; set; }
    }
}
