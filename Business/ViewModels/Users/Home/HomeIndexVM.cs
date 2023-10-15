using Business.ViewModels.Users.Blog;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ViewModels.Users.Home
{
    public class HomeIndexVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Category> Categories { get; set; }
        public List<Category> CategoryTake3 { get; set; }
        public List<OnSale_1Component> onSaleComponents { get; set; }
        public List<OnSale_2Component> onSale_2Components { get; set; }
        public List<Common.Entities.Blog> Blogs { get; set; }
        public List<Product> AllProducts { get; set; }
        //public Category Category { get; set; }


        //Tab Menu Products
        public List<Product> FilteredProducts { get; set; }

        public int Id { get; set; }
    }
}
