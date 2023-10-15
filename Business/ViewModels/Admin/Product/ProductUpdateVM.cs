using Common.Entities;
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
    public class ProductUpdateVM
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
        public string? MainPhotoPath { get; set; }
        public IFormFile? MainPhoto { get; set; }
        public List<IFormFile>? Photos { get; set; }
        public List<ProductPhoto>? PhotoNames { get; set; }
        public string? Status { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal? NewPrice { get; set; }
        [Required]
        [Display(Name = "SubCategory")]
        public int SubCategoryId { get; set; }
        public List<SelectListItem>? SubCategories { get; set; }
        [Display(Name = "Special Product")]
        public bool IsSpecial { get; set; }
        [Display(Name = "Main Product")]
        public bool IsMain { get; set; }
    }
}
