using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Product
{
    public class ProductIndexVM
    {
        public ProductIndexVM()
        {
            Products = new List<Common.Entities.Product>();
        }
        public List<Common.Entities.Product> Products { get; set; }
        public bool IsChecked { get; set; }
    }
}
