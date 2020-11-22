using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizBuildingServices.Data.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
