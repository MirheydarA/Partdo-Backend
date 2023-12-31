﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.OnSale_1
{
    public class OnSale_1CreateVM
    {
        [Required, MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
        [MaxLength(60, ErrorMessage = "Title must be maximum 60 characters")]
        public string Title { get; set; }
        [Required, MinLength(5, ErrorMessage = "Description must be at least 5 characters")]
        [MaxLength(60, ErrorMessage = "Description must be maximum 60 characters")]
        public string Description { get; set; }
        [Required]
        public IFormFile PhotoName { get; set; }
    }
}
