using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Users.Message
{
    public class MessageVM
    {
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
        [MaxLength(38, ErrorMessage = "Name must be maximum 38 characters")]
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string SenderName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email format is not correct")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string SenderGmail { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
