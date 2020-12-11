using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizBuildingServices.Data.Models
{
    public class TenantComplaint
    {
        public int PropertyId { get; set; }
        public int CategoryId { get; set; }
        public string Location { get; set; }
        public string GeoLocation { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

    }
    public class TenantLog : TenantComplaint
    {
        public int LogId { get; set; }
        public string CategoryName { get; set; }
        public int? AssignedTo { get; set; }
        public string AssignedName { get; set; }
        public DateTime RequestedDate { get; set; }
        public DateTime? ResolveDate { get; set; }
    }
    public class LogStatus
    {
        public int LogId { get; set; }
        public string Status { get; set; }
        public int? AssignedTo { get; set; }
    }
}
