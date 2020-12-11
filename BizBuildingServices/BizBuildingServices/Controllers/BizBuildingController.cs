using BizBuildingServices.Data.Interfaces;
using BizBuildingServices.Data.Models;
using BizBuildingServices.Data.Models.ViewModels;
using BizBuildingServices.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BizBuildingServices.Controllers
{
    public class BizBuildingController : ApiController
    {
        private IPropertyUser _repository;
        public BizBuildingController(IPropertyUser repository)
        {
            this._repository = repository;
        }
        [HttpGet]
        public string GenerateQRCode(int Id)
        {
            QRHelper.UploadFileToS3(Id.ToString());
            return "";
        }
        [HttpPost]
        public HttpResponseMessage Signup(PropertySignUp propertySignUp)
        {
            int propertyId;
            int userId = _repository.PropertySignUp(propertySignUp, out propertyId);
            if (userId > 0)
            {
                QRHelper.UploadFileToS3(propertyId.ToString());
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else if (userId == -1)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format(Resources.Messages.EmailAddressExists, propertySignUp.propertyUser.EmailAddress));
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.Messages.InvalidData);
        }
        [HttpPost]
        public HttpResponseMessage GetSignInInformation(Login login)
        {
            int propertyId;
            int userId = _repository.GetSigninInformation(login, out propertyId);
            if (userId > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    UserId = userId,
                    PropertyId = propertyId
                });
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.Messages.InvalidSignIn);
        }
        [HttpGet]
        public HttpResponseMessage GetUserInformation(int Id)
        {
            PropertyUser user = _repository.GetUserInformation(Id);
            if (user != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "N");
        }
        [HttpGet]
        public HttpResponseMessage GetLogs(int Id)
        {
            List<TenantLog> logs = _repository.GetAllLogs(Id);
            if (logs.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, logs);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.Messages.NoLogs);
        }
        [HttpDelete]
        public HttpResponseMessage DeleteLog(int Id)
        {
            int logId = _repository.DeleteLog(Id);
            if (logId > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.Messages.NoLogs);
        }
        [HttpPost]
        public HttpResponseMessage UpdateLogStatus(LogStatus category)
        {
            int logId = _repository.UpdateLogStatus(category);
            if (logId > 0)
            {
                StatusInfo objStatus = new StatusInfo();
                objStatus.Message = Resources.Messages.StatusSuccess;
                return Request.CreateResponse(HttpStatusCode.OK, objStatus);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.Messages.InvalidData);
        }
        [HttpPost]
        public HttpResponseMessage SaveCompliant(TenantComplaint tenantComplaint)
        {
            int logId = _repository.SaveTenantComplaint(tenantComplaint);
            if (logId > 0)
            {
                StatusInfo objStatus = new StatusInfo();
                objStatus.Message = Resources.Messages.StatusSuccess;
                return Request.CreateResponse(HttpStatusCode.OK, objStatus);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.Messages.InvalidData);
        }
        [HttpGet]
        public HttpResponseMessage GetLogInformation(int Id)
        {
            LogInformation objLogInfo = _repository.GetLogInformation(Id);
            if (objLogInfo.Users.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, objLogInfo);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.Messages.InvalidData);
        }
        [HttpPost]
        public HttpResponseMessage SaveStaff(PropertyUser user)
        {
            int userId = _repository.SaveStaffInformation(user);
            if (userId > 0)
            {
                StatusInfo objStatus = new StatusInfo();
                objStatus.Message = string.Format(Resources.Messages.SaveSuccess, "Staff");
                dynamic info = new
                {
                    UserId = userId
                };
                objStatus.Info = info;
                return Request.CreateResponse(HttpStatusCode.OK, objStatus);
            }
            else if (userId == -1)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format(Resources.Messages.EmailAddressExists, user.EmailAddress));
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.Messages.InvalidData);
        }
        [HttpGet]
        public HttpResponseMessage GetStaffList(int Id)
        {
            List<PropertyUser> staffs = _repository.GetStaffList(Id);
            if (staffs.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, staffs);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.Messages.NoStaff);
        }
        [HttpDelete]
        public HttpResponseMessage DeleteStaff(int Id)
        {
            int categoryId = _repository.DeleteStaff(Id);
            if (categoryId > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.Messages.NoLogs);
        }
        [HttpPost]
        public HttpResponseMessage SaveCategory(Category category)
        {
            int categoryId = _repository.SaveCategory(category);
            if (categoryId > 0)
            {
                StatusInfo objStatus = new StatusInfo();
                objStatus.Message = string.Format(Resources.Messages.SaveSuccess, "Catgory");
                dynamic info = new
                {
                    CategoryId = categoryId
                };
                objStatus.Info = info;
                return Request.CreateResponse(HttpStatusCode.OK, objStatus);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.Messages.InvalidData);
        }
        [HttpGet]
        public HttpResponseMessage GetCategoriesList(int Id)
        {
            List<Category> categories = _repository.GetCategoriesList(Id);
            if (categories.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, categories);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.Messages.NoCategories);
        }
        [HttpGet]
        public HttpResponseMessage GetCategoryInformation(int Id)
        {
            Category category = _repository.GetCategoryInformation(Id);
            if (category != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, category);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No category available.");
        }
        [HttpDelete]
        public HttpResponseMessage DeleteCategory(int Id)
        {
            int categoryId = _repository.DeleteCategory(Id);
            if (categoryId > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.Messages.NoLogs);
        }
        [HttpGet]
        public HttpResponseMessage GetPropertyInformation(int Id)
        {
            Property property = _repository.GetPropertyInformation(Id);
            if (property != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, property);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No category available.");
        }
        [HttpPost]
        public HttpResponseMessage SavePropertyInformation(Property property)
        {
            int propertyId = _repository.SavePropertyInformation(property);
            if (propertyId > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.Messages.InvalidData);
        }
    }
}
