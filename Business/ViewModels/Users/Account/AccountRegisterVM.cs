using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Users.Account
{
    public class AccountRegisterVM
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(36, ErrorMessage = "Must be between 3 and 16 characters", MinimumLength = 3)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [StringLength(36, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
