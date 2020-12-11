using BizBuildingServices.Data.Models;
using BizBuildingServices.Data.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizBuildingServices.Data.Interfaces
{
    public interface IPropertyUser
    {
        int PropertySignUp(PropertySignUp user, out int propertyId);
        int GetSigninInformation(Login login, out int propertyId);
        PropertyUser GetUserInformation(int Id);
        int SaveStaffInformation(PropertyUser user);
        List<PropertyUser> GetStaffList(int Id);
        int DeleteStaff(int Id);
        int SaveCategory(Category category);
        List<Category> GetCategoriesList(int Id);
        Category GetCategoryInformation(int Id);
        int DeleteCategory(int Id);
        int SaveTenantComplaint(TenantComplaint tenantComplaint);
        List<TenantLog> GetAllLogs(int Id);
        LogInformation GetLogInformation(int LogId);
        int UpdateLogStatus(LogStatus logStatus);
        int DeleteLog(int Id);
        Property GetPropertyInformation(int Id);
        int SavePropertyInformation(Property property);
    }
}
