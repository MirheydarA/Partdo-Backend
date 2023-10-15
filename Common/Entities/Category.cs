using Common.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Category : BaseEntity
    { 
        public string Name { get; set; }
        public string CoverPhoto { get; set; }
        public List<SubCategory> SubCategories { get; set; }
    }
}
