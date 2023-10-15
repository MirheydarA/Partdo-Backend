using Common.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Admin.Slider
{
    public class SliderIndexVM
    {
        public SliderIndexVM()
        {
            Sliders = new List<Common.Entities.Slider>();
        }

        public List<Common.Entities.Slider> Sliders { get; set; }
    }
}
