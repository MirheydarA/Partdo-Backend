using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.OnSale_1
{
    public class OnSale_1UpdateVM
    {
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
        [MaxLength(38, ErrorMessage = "Title must be maximum 60 characters")]
        public string Title { get; set; }
        [MinLength(5, ErrorMessage = "Description must be at least 5 characters")]
        [MaxLength(38, ErrorMessage = "Description must be maximum 60 characters")]
        public string Description { get; set; }
        public IFormFile? Photo { get; set; }
        public string? PhotoPath { get; set; }

    }
}
