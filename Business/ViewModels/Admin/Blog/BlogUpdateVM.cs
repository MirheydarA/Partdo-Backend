using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Blog
{
    public class BlogUpdateVM
    {
        [Required, MinLength(3, ErrorMessage = "Title must be at least 3 charachter")]
        public string Title { get; set; }
        [Required, MinLength(3, ErrorMessage = "Shortdesc must be at least 3 charachter")]
        public string ShortDesc { get; set; }
        [MinLength(3, ErrorMessage = "Description must be at least 3 charachter")]
        public string Description { get; set; }
        [Required]
        public string Tags { get; set; }
        public string Author { get; set; }
        [Required]
        public string PhotoPath { get; set; }
        public IFormFile? Photo { get; set; }
        [Display(Name = "Category")]
        public string Category { get; set; }
    }
}
