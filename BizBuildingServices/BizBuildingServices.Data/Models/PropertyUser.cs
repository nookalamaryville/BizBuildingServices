using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizBuildingServices.Data.Models
{
    public class PropertyUser
    {
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string UserType { get; set; }
        public string FullName { get { return this.FirstName + " " + this.LastName; } }
    }
    public class Login
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
