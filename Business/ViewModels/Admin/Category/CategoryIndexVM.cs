using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Category
{
    public class CategoryIndexVM
    {
        public CategoryIndexVM()
        {
            Categories = new List<Common.Entities.Category>();
        }

        public List<Common.Entities.Category> Categories { get; set; }
    }
}
