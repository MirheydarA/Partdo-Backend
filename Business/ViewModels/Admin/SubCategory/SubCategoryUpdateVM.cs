using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.SubCategory
{
    public class SubCategoryUpdateVM
    {
        [Required, MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
        [MaxLength(20, ErrorMessage = "Title must be maximum 20 characters")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }
        public List<SelectListItem>? Categories { get; set; }
    }
}
