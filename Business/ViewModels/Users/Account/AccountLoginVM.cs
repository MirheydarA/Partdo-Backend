using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Users.Account
{
    public class AccountLoginVM
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
