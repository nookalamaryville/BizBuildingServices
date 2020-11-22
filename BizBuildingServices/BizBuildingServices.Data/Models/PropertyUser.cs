using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizBuildingServices.Data.Models
{
    public class PropertyUser : Property
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
    }
    public class Login
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
