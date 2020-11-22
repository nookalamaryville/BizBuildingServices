using BizBuildingServices.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizBuildingServices.Data.Interfaces
{
    public interface IPropertyUser
    {
        int PropertySignUp(PropertyUser user);
        int GetSigninInformation(Login login);
        PropertyUser GetUserInformation(int userId);
        int SaveStaffInformation(PropertyUser user);
        List<PropertyUser> GetStaffList(int propertyId);
        int SaveCategory(Category category);
        List<Category> GetCategoriesList(int propertyId);
        List<TenantLog> GetAllLogs(int propertyid);
        int UpdateLogStatus(LogStatus logStatus);
        int SaveTenantLog(TenantLog tenantLog);
    }
}
