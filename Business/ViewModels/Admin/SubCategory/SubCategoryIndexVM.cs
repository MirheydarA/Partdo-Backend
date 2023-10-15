using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.SubCategory
{
    public class SubCategoryIndexVM
    {
        public SubCategoryIndexVM()
        {
            SubCategories = new List<Common.Entities.SubCategory>();
        }

        public List<Common.Entities.SubCategory> SubCategories { get; set; }
    }
}
