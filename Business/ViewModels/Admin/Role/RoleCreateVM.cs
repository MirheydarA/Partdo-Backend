using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Role
{
    public class RoleCreateVM
    {
        [Required, MinLength(2, ErrorMessage ="Name must be at least 2 charachter")]
        [MaxLength(15, ErrorMessage = "Name must be maximum 15 charachter")]
        public string Name { get; set; }
    }
}
