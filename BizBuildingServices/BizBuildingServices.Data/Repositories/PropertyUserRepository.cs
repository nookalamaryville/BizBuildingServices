using BizBuildingServices.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizBuildingServices.Data.Models;
using Npgsql;
using System.Configuration;
using System.Data;
using BizBuildingServices.Data.Models.ViewModels;
using System.Data.SqlClient;

namespace BizBuildingServices.Data.Repositories
{
    public class PropertyUserRepository : IPropertyUser
    {
        public int PropertySignUp(PropertySignUp user, out int propertyId)
        {
            propertyId = 0;
            int userId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("uspPropertySignUp", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Name", user.property.Name));
                    cmd.Parameters.Add(new SqlParameter("@FName", user.propertyUser.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LName", user.propertyUser.LastName));
                    cmd.Parameters.Add(new SqlParameter("@EmailAddress", user.propertyUser.EmailAddress));
                    cmd.Parameters.Add(new SqlParameter("@Password", user.propertyUser.Password));
                    cmd.Parameters.Add(new SqlParameter("@UserType", user.propertyUser.UserType));
                    cmd.Parameters.Add("@PropertyId", SqlDbType.Int, 20);
                    cmd.Parameters["@PropertyId"].Direction = ParameterDirection.Output;
                    userId = (int)cmd.ExecuteScalar();
                    propertyId = (int)cmd.Parameters["@PropertyId"].Value;
                    cmd.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return userId;
        }
        public int GetSigninInformation(Login login, out int propertyId)
        {
            int userId = 0;
            propertyId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("uspGetSigninInformation", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@EmailAddress", login.EmailAddress));
                    cmd.Parameters.Add(new SqlParameter("@Password", login.Password));
                    cmd.Parameters.Add("@PropertyId", SqlDbType.Int, 20);
                    cmd.Parameters["@PropertyId"].Direction = ParameterDirection.Output;                    
                    userId = (int)cmd.ExecuteScalar();
                    propertyId = (int)cmd.Parameters["@PropertyId"].Value;
                    cmd.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return userId;
        }
        public PropertyUser GetUserInformation(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("uspGetUserInformation", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@UserId", Id));
                    IDataReader dr = cmd.ExecuteReader();
                    PropertyUser user = new PropertyUser();
                    while (dr.Read())
                    {
                        user.FirstName = dr["first_name"] == System.DBNull.Value ? "" : Convert.ToString(dr["first_name"]);
                        user.LastName = dr["last_name"] == System.DBNull.Value ? "" : Convert.ToString(dr["last_name"]);
                        user.EmailAddress = dr["email_address"] == System.DBNull.Value ? "" : Convert.ToString(dr["email_address"]);
                        user.Password = dr["password"] == System.DBNull.Value ? "" : Convert.ToString(dr["password"]);
                        user.PhoneNumber = dr["phone_number"] == System.DBNull.Value ? "" : Convert.ToString(dr["phone_number"]);
                        user.UserType = dr["user_type"] == System.DBNull.Value ? "" : Convert.ToString(dr["user_type"]);
                    }
                    dr.Close();
                    cmd.Dispose();
                    connection.Close();
                    return user;
                }
            }
            catch (Exception ex) { }
            return null;
        }
        public List<PropertyUser> GetStaffList(int Id)
        {
            List<PropertyUser> objStaffList = new List<PropertyUser>();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("uspGetStaffList", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PropertyId", Id));
                    IDataReader dr = cmd.ExecuteReader();
                    PropertyUser user;
                    while (dr.Read())
                    {
                        user = new PropertyUser();
                        user.UserId = dr["user_id"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["user_id"]);
                        user.FirstName = dr["first_name"] == System.DBNull.Value ? "" : Convert.ToString(dr["first_name"]);
                        user.LastName = dr["last_name"] == System.DBNull.Value ? "" : Convert.ToString(dr["last_name"]);
                        user.EmailAddress = dr["email_address"] == System.DBNull.Value ? "" : Convert.ToString(dr["email_address"]);
                        user.PhoneNumber = dr["phone_number"] == System.DBNull.Value ? "" : Convert.ToString(dr["phone_number"]);
                        user.UserType = dr["user_type"] == System.DBNull.Value ? "" : Convert.ToString(dr["user_type"]);
                        objStaffList.Add(user);
                    }
                    dr.Close();
                    cmd.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return objStaffList;
        }
        public int SaveStaffInformation(PropertyUser user)
        {
            int userId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("uspSaveStaffInformation", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PropertyId", Convert.ToInt32(user.PropertyId)));
                    cmd.Parameters.Add(new SqlParameter("@UserId", Convert.ToInt32(user.UserId)));
                    cmd.Parameters.Add(new SqlParameter("@FName", user.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LName", user.LastName));
                    cmd.Parameters.Add(new SqlParameter("@EmailAddress", user.EmailAddress));
                    cmd.Parameters.Add(new SqlParameter("@PhoneNumber", user.PhoneNumber));
                    cmd.Parameters.Add(new SqlParameter("@UserType", user.UserType));
                    cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
                    userId = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return userId;
        }
        public int DeleteStaff(int Id)
        {
            int userId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("uspDeleteStaff", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@UserId", Id));
                    cmd.ExecuteNonQuery();
                    userId = Id;
                    cmd.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return userId;
        }
        public int SaveCategory(Category category)
        {
            int categoryId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("uspSaveCategory", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", Convert.ToInt32(category.CategoryId)));
                    cmd.Parameters.Add(new SqlParameter("@PropertyId", Convert.ToInt32(category.PropertyId)));
                    cmd.Parameters.Add(new SqlParameter("@Name", category.Name));
                    cmd.Parameters.Add(new SqlParameter("@Description", category.Description));
                    categoryId = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return categoryId;
        }
        public List<Category> GetCategoriesList(int Id)
        {
            List<Category> objCategoriesList = new List<Category>();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("uspGetAllCategories", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PropertyId", Id));
                    IDataReader dr = cmd.ExecuteReader();
                    Category category;
                    while (dr.Read())
                    {
                        category = new Category();
                        category.CategoryId = dr["category_id"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["category_id"]);
                        category.Name = dr["name"] == System.DBNull.Value ? "" : Convert.ToString(dr["name"]);
                        category.Description = dr["description"] == System.DBNull.Value ? "" : Convert.ToString(dr["description"]);
                        objCategoriesList.Add(category);
                    }
                    dr.Close();
                    cmd.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return objCategoriesList;
        }
        public Category GetCategoryInformation(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("uspGetCategoryById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", Id));
                    IDataReader dr = cmd.ExecuteReader();
                    Category user = new Category();
                    while (dr.Read())
                    {
                        user.Name = dr["name"] == System.DBNull.Value ? "" : Convert.ToString(dr["name"]);
                        user.Description = dr["description"] == System.DBNull.Value ? "" : Convert.ToString(dr["description"]);
                    }
                    dr.Close();
                    cmd.Dispose();
                    connection.Close();
                    return user;
                }
            }
            catch (Exception ex) { }
            return null;
        }
        public int DeleteCategory(int Id)
        {
            int categoryId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("uspDeleteCategory", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", Id));
                    cmd.ExecuteNonQuery();
                    categoryId = Id;
                    cmd.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return categoryId;
        }
        public List<TenantLog> GetAllLogs(int Id)
        {
            List<TenantLog> objTenantLogsList = new List<TenantLog>();
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("uspGetAllLogs", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PropertyId", Id));
                    IDataReader dr = cmd.ExecuteReader();
                    TenantLog tenantLog;
                    while (dr.Read())
                    {
                        tenantLog = new TenantLog();
                        tenantLog.LogId = dr["log_id"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["log_id"]);
                        tenantLog.Location = dr["location"] == System.DBNull.Value ? "" : Convert.ToString(dr["location"]);
                        tenantLog.GeoLocation = dr["geolocation"] == System.DBNull.Value ? "" : Convert.ToString(dr["geolocation"]);
                        tenantLog.Description = dr["description"] == System.DBNull.Value ? "" : Convert.ToString(dr["description"]);
                        tenantLog.AssignedTo = dr["assigned_to"] == System.DBNull.Value ? (int?)null : Convert.ToInt32(dr["assigned_to"]);
                        tenantLog.AssignedName = dr["assignedname"] == System.DBNull.Value ? "" : Convert.ToString(dr["assignedname"]).TrimEnd();
                        tenantLog.Status = dr["status"] == System.DBNull.Value ? "" : Convert.ToString(dr["status"]);
                        tenantLog.CategoryName = dr["categoryname"] == System.DBNull.Value ? "" : Convert.ToString(dr["categoryname"]);
                        tenantLog.RequestedDate = dr["requested_date"] == System.DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["requested_date"]);
                        tenantLog.ResolveDate = dr["resolve_date"] == System.DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["resolve_date"]);
                        objTenantLogsList.Add(tenantLog);
                    }
                    dr.Close();
                    cmd.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return objTenantLogsList;
        }
        public int SaveTenantComplaint(TenantComplaint tenantComplaint)
        {
            int logId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("uspSaveTenantComplaint", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PropertyId", tenantComplaint.PropertyId));
                    cmd.Parameters.Add(new SqlParameter("@CategoryId", tenantComplaint.CategoryId));
                    cmd.Parameters.Add(new SqlParameter("@Location", tenantComplaint.Location));
                    cmd.Parameters.Add(new SqlParameter("@GeoLocation", tenantComplaint.GeoLocation));
                    cmd.Parameters.Add(new SqlParameter("@Description", tenantComplaint.Description));
                    cmd.Parameters.Add(new SqlParameter("@Status", tenantComplaint.Status));
                    logId = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return logId;
        }
        public LogInformation GetLogInformation(int LogId)
        {
            try
            {
                LogInformation logInformation = new LogInformation();
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("uspGetLogInformation", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@LogId", LogId));
                    IDataReader dr = cmd.ExecuteReader();
                    List<PropertyUser> objUsers = new List<PropertyUser>();
                    PropertyUser user;
                    while (dr.Read())
                    {
                        user = new PropertyUser();
                        user.UserId = dr["user_id"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["user_id"]);
                        user.FirstName = dr["first_name"] == System.DBNull.Value ? "" : Convert.ToString(dr["first_name"]);
                        user.LastName = dr["last_name"] == System.DBNull.Value ? "" : Convert.ToString(dr["last_name"]);
                        objUsers.Add(user);
                    }
                    dr.NextResult();
                    TenantLog objTenantLog = new TenantLog();
                    while (dr.Read())
                    {
                        objTenantLog.Location = dr["location"] == System.DBNull.Value ? "" : Convert.ToString(dr["location"]);
                        objTenantLog.GeoLocation = dr["geolocation"] == System.DBNull.Value ? "" : Convert.ToString(dr["geolocation"]);
                        objTenantLog.Description = dr["description"] == System.DBNull.Value ? "" : Convert.ToString(dr["description"]);
                        objTenantLog.AssignedTo = dr["assigned_to"] == System.DBNull.Value ? (int?)null : Convert.ToInt32(dr["assigned_to"]);
                        objTenantLog.AssignedName = dr["assignedname"] == System.DBNull.Value ? "" : Convert.ToString(dr["assignedname"]).TrimEnd();
                        objTenantLog.Status = dr["status"] == System.DBNull.Value ? "" : Convert.ToString(dr["status"]);
                        objTenantLog.CategoryName = dr["categoryname"] == System.DBNull.Value ? "" : Convert.ToString(dr["categoryname"]);
                        objTenantLog.RequestedDate = dr["requested_date"] == System.DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["requested_date"]);
                        objTenantLog.ResolveDate = dr["resolve_date"] == System.DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["resolve_date"]);
                    }
                    dr.Close();
                    cmd.Dispose();
                    connection.Close();
                    logInformation.Users = objUsers;
                    logInformation.TenantComplaint = objTenantLog;
                    return logInformation;
                }
            }
            catch (Exception ex) { }
            return null;
        }
        public int UpdateLogStatus(LogStatus logStatus)
        {
            int logId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("uspUpdateLogStatus", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@LogId", logStatus.LogId));
                    cmd.Parameters.Add(new SqlParameter("@Status", logStatus.Status));
                    cmd.Parameters.Add(new SqlParameter("@AssignedTo", logStatus.AssignedTo));
                    cmd.ExecuteNonQuery();
                    logId = logStatus.LogId;
                    cmd.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return logId;
        }
        public int DeleteLog(int Id)
        {
            int logId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("uspDeleteLog", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@LogId", Id));
                    cmd.ExecuteNonQuery();
                    logId = Id;
                    cmd.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return logId;
        }
        public Property GetPropertyInformation(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("uspGetPropertyInformation", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PropertyId", Id));
                    IDataReader dr = cmd.ExecuteReader();
                    Property proeprty = new Property();
                    while (dr.Read())
                    {
                        proeprty.Name = dr["property_name"] == System.DBNull.Value ? "" : Convert.ToString(dr["property_name"]);
                        proeprty.Address = dr["address"] == System.DBNull.Value ? "" : Convert.ToString(dr["address"]);
                        proeprty.City = dr["city"] == System.DBNull.Value ? "" : Convert.ToString(dr["city"]);
                        proeprty.State = dr["state"] == System.DBNull.Value ? "" : Convert.ToString(dr["state"]);
                        proeprty.Zipcode = dr["zipcode"] == System.DBNull.Value ? "" : Convert.ToString(dr["zipcode"]);
                    }
                    dr.Close();
                    cmd.Dispose();
                    connection.Close();
                    return proeprty;
                }
            }
            catch (Exception ex) { }
            return null;
        }
        public int SavePropertyInformation(Property property)
        {
            int propertyId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("uspSavePropertyInformation", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PropertyId", Convert.ToInt32(property.PropertyId)));
                    cmd.Parameters.Add(new SqlParameter("@Name", property.Name));
                    cmd.Parameters.Add(new SqlParameter("@Address", property.Address));
                    cmd.Parameters.Add(new SqlParameter("@City", property.City));
                    cmd.Parameters.Add(new SqlParameter("@State", property.State));
                    cmd.Parameters.Add(new SqlParameter("@Zipcode", property.Zipcode));
                    propertyId = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return propertyId;
        }
    }
}
