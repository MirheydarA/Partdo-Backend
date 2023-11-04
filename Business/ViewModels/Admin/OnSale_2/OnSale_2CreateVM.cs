using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.OnSale_2
{
    public class OnSale_2CreateVM
    {
        [Required, MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
        [MaxLength(80, ErrorMessage = "Title must be maximum 80 characters")]
        public string Title { get; set; }
        [Required, MinLength(5, ErrorMessage = "Description must be at least 5 characters")]
        [MaxLength(80, ErrorMessage = "Description must be maximum 80 characters")]
        public string Description { get; set; }
        [Required]
        public IFormFile PhotoName { get; set; }
    }
}
