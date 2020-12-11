using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizBuildingServices.Data.Models.ViewModels
{
    public class LogInformation
    {
        public List<PropertyUser> Users { get; set; }
        public TenantLog TenantComplaint { get; set; }
    }
}
