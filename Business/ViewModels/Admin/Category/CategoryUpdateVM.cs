using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Category
{
    public class CategoryUpdateVM
    {
        [Required, MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
        [MaxLength(38, ErrorMessage = "Title must be maximum 38 characters")]
        public string Name { get; set; }
        public IFormFile? CoverPhoto { get; set; }
        public string? CoverPhotoPath { get; set; }
    }
}
