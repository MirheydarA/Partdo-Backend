using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Constants
{
    public enum ProductStock
    {
        [Display(Name ="In Stock")]
        InStock,
        [Display(Name = "Out of Stock")]
        OutOfStock
    }
}
