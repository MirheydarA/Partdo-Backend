using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.OnSale_1
{
    public class OnSale_1IndexVM
    {
        public OnSale_1IndexVM()
        {
            OnSale_1s = new List<Common.Entities.OnSale_1Component>();
        }

        public List<OnSale_1Component> OnSale_1s { get; set; }
    }
}
