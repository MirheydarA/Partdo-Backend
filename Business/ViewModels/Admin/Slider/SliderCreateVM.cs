using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Slider
{
    public class SliderCreateVM
    {
        [Required(ErrorMessage = "Please enter photo")]
        public IFormFile PhotoName { get; set; }
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
        [MaxLength(38, ErrorMessage = "Title must be maximum 38 characters")]
        public string Title { get; set; }
        [MinLength(10, ErrorMessage = "Title must be at least 10 characters")]
        public string Description { get; set; }
    }
}
