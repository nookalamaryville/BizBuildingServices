using BizBuildingServices.Data.Interfaces;
using BizBuildingServices.Data.Models;
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
        [HttpPost]
        public HttpResponseMessage Signup(PropertyUser propertyUser)
        {
            int userId = _repository.PropertySignUp(propertyUser);
            if (userId > 0)
            {
                StatusInfo objStatus = new StatusInfo();
                objStatus.Message = Resources.Messages.SignupSuccess;
                dynamic info = new
                {
                    UserId = userId
                };
                objStatus.Info = info;
                return Request.CreateResponse(HttpStatusCode.OK, objStatus);
            }
            else if(userId == -1)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format(Resources.Messages.EmailAddressExists, propertyUser.EmailAddress));
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.Messages.InvalidData);
        }
        [HttpPost]
        public HttpResponseMessage GetSignInInformation(Login login)
        {
            int userId = _repository.GetSigninInformation(login);
            if (userId > 0)
            {
                StatusInfo objStatus = new StatusInfo();
                dynamic info = new
                {
                    UserId = userId
                };
                objStatus.Info = info;
                return Request.CreateResponse(HttpStatusCode.OK, objStatus);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.Messages.InvalidSignIn);
        }
        [HttpGet]
        public HttpResponseMessage GetUserInformation(int userId)
        {
            PropertyUser user = _repository.GetUserInformation(userId);
            if (user.UserId > 0)
            {
                StatusInfo objStatus = new StatusInfo();
                dynamic info = new
                {
                    User = user
                };
                objStatus.Info = info;
                return Request.CreateResponse(HttpStatusCode.OK, objStatus);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "N");
        }
        [HttpPost]
        public HttpResponseMessage AddStaff(PropertyUser user)
        {
            int userId =_repository.SaveStaffInformation(user);
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
            else if(userId == -1)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format(Resources.Messages.EmailAddressExists, user.EmailAddress));
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.Messages.InvalidData);
        }
        [HttpGet]
        public HttpResponseMessage GetStaffs(int propertyId)
        {
            List<PropertyUser> staffs = _repository.GetStaffList(propertyId);
            if (staffs.Count > 0)
            {
                StatusInfo objStatus = new StatusInfo();
                dynamic info = new
                {
                    Staff = staffs
                };
                objStatus.Info = info;
                return Request.CreateResponse(HttpStatusCode.OK, objStatus);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.Messages.NoStaff);
        }
        [HttpPost]
        public HttpResponseMessage AddCategory(Category category)
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
        public HttpResponseMessage GetCategories(int propertyId)
        {
            List<Category> categories = _repository.GetCategoriesList(propertyId);
            if (categories.Count > 0)
            {
                StatusInfo objStatus = new StatusInfo();
                dynamic info = new
                {
                    Categories = categories
                };
                objStatus.Info = info;
                return Request.CreateResponse(HttpStatusCode.OK, objStatus);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.Messages.NoCategories);
        }
        [HttpGet]
        public HttpResponseMessage GetLogs(int propertyId)
        {
            List<TenantLog> logs = _repository.GetAllLogs(propertyId);
            if (logs.Count > 0)
            {
                StatusInfo objStatus = new StatusInfo();
                dynamic info = new
                {
                    Logs = logs
                };
                objStatus.Info = info;
                return Request.CreateResponse(HttpStatusCode.OK, objStatus);
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
        public HttpResponseMessage SaveCompliant(TenantLog tenantLog)
        {
            int logId = _repository.SaveTenantLog(tenantLog);
            if (logId > 0)
            {
                StatusInfo objStatus = new StatusInfo();
                objStatus.Message = Resources.Messages.StatusSuccess;
                return Request.CreateResponse(HttpStatusCode.OK, objStatus);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Resources.Messages.LogFailed);
        }
    }
}
