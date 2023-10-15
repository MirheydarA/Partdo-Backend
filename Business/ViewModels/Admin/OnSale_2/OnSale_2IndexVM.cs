using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.OnSale_2
{
    public class OnSale_2IndexVM
    {
        public OnSale_2IndexVM()
        {
            OnSale_2s = new List<Common.Entities.OnSale_2Component>();
        }

        public List<OnSale_2Component> OnSale_2s { get; set; }
    }
}
