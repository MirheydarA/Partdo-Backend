using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.User
{
    public class UserIndexVM
    {
        public UserIndexVM()
        {
            Users = new List<UserVM>();
        }

        public List<UserVM> Users { get;  set; }
    }
}
