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

namespace BizBuildingServices.Data.Repositories
{
    public class PropertyUserRepository : IPropertyUser
    {
        public int PropertySignUp(PropertyUser user)
        {
            int userId = 0;
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("usp_propertysignup", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new NpgsqlParameter("@PropertyId", Convert.ToInt32(user.PropertyId)));
                    cmd.Parameters.Add(new NpgsqlParameter("@Name", user.Name));
                    cmd.Parameters.Add(new NpgsqlParameter("@FName", user.FirstName));
                    cmd.Parameters.Add(new NpgsqlParameter("@LName", user.LastName));
                    cmd.Parameters.Add(new NpgsqlParameter("@EmailAddress", user.EmailAddress));
                    cmd.Parameters.Add(new NpgsqlParameter("@Password", user.Password));
                    cmd.Parameters.Add(new NpgsqlParameter("@UserType", user.UserType));
                    userId = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return userId;
        }
        public int GetSigninInformation(Login login)
        {
            int userId = 0;
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("usp_getsignininformation", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new NpgsqlParameter("@EmailAddress", login.EmailAddress));
                    cmd.Parameters.Add(new NpgsqlParameter("@Password", login.Password));
                    userId = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return userId;
        }
        public PropertyUser GetUserInformation(int userId)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("usp_getuserinformation", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new NpgsqlParameter("@UserId", userId));
                    IDataReader dr = cmd.ExecuteReader();
                    PropertyUser user = new PropertyUser();
                    while (dr.Read())
                    {
                        user.PropertyId = dr["property_id"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["property_id"]);
                        user.Name = dr["name"] == System.DBNull.Value ? "" : Convert.ToString(dr["name"]);
                        user.UserId = dr["Rating"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["Rating"]);
                        user.FirstName = dr["first_name"] == System.DBNull.Value ? "" : Convert.ToString(dr["first_name"]);
                        user.LastName = dr["last_name"] == System.DBNull.Value ? "" : Convert.ToString(dr["last_name"]);
                        user.EmailAddress = dr["email_address"] == System.DBNull.Value ? "" : Convert.ToString(dr["email_address"]);
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
        public int SaveStaffInformation(PropertyUser user)
        {
            int userId = 0;
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("usp_propertystaff", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new NpgsqlParameter("@PropertyId", Convert.ToInt32(user.PropertyId)));
                    cmd.Parameters.Add(new NpgsqlParameter("@FName", user.FirstName));
                    cmd.Parameters.Add(new NpgsqlParameter("@LName", user.LastName));
                    cmd.Parameters.Add(new NpgsqlParameter("@EmailAddress", user.EmailAddress));
                    cmd.Parameters.Add(new NpgsqlParameter("@Password", user.Password));
                    cmd.Parameters.Add(new NpgsqlParameter("@UserType", user.UserType));
                    userId = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return userId;
        }
        public List<PropertyUser> GetStaffList(int propertyId)
        {
            List<PropertyUser> objStaffList = new List<PropertyUser>();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("usp_PropertySignup", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    IDataReader dr = cmd.ExecuteReader();
                    PropertyUser user;
                    while (dr.Read())
                    {
                        user = new PropertyUser();
                        user.UserId = dr["user_id"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["user_id"]);
                        user.FirstName = dr["MovieName"] == System.DBNull.Value ? "" : Convert.ToString(dr["MovieName"]);
                        user.LastName = dr["Launguage"] == System.DBNull.Value ? "" : Convert.ToString(dr["Launguage"]);
                        user.EmailAddress = dr["Genre"] == System.DBNull.Value ? "" : Convert.ToString(dr["Genre"]);
                        user.UserType = dr["Director"] == System.DBNull.Value ? "" : Convert.ToString(dr["Director"]);
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
        public int SaveCategory(Category category)
        {
            int categoryId = 0;
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("usp_savecategory", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new NpgsqlParameter("@PropertyId", Convert.ToInt32(category.PropertyId)));
                    cmd.Parameters.Add(new NpgsqlParameter("@Name", category.Name));
                    cmd.Parameters.Add(new NpgsqlParameter("@Description", category.Description));
                    categoryId = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return categoryId;
        }
        public List<Category> GetCategoriesList(int propertyId)
        {
            List<Category> objCategoriesList = new List<Category>();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("uspPropertySignup", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    IDataReader dr = cmd.ExecuteReader();
                    Category category;
                    while (dr.Read())
                    {
                        category = new Category();
                        category.CategoryId = dr["category_id"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["category_id"]);
                        category.Name = dr["name"] == System.DBNull.Value ? "" : Convert.ToString(dr["name"]);
                        category.Description = dr["Genre"] == System.DBNull.Value ? "" : Convert.ToString(dr["Genre"]);
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
        public List<TenantLog> GetAllLogs(int propertyId)
        {
            List<TenantLog> objTenantLogsList = new List<TenantLog>();
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("uspPropertySignup", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    IDataReader dr = cmd.ExecuteReader();
                    TenantLog tenantLog;
                    while (dr.Read())
                    {
                        tenantLog = new TenantLog();
                        tenantLog.LogId = dr["log_id"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["log_id"]);
                        tenantLog.Description = dr["description"] == System.DBNull.Value ? "" : Convert.ToString(dr["description"]);
                        tenantLog.Location = dr["location"] == System.DBNull.Value ? "" : Convert.ToString(dr["location"]);
                        tenantLog.AssignedTo = dr["assigned_to"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["assigned_to"]);
                        tenantLog.AssignedName = dr["assignedname"] == System.DBNull.Value ? "" : Convert.ToString(dr["assignedname"]);
                        tenantLog.Status = dr["Genre"] == System.DBNull.Value ? "" : Convert.ToString(dr["Genre"]);
                        tenantLog.CategoryName = dr["name"] == System.DBNull.Value ? "" : Convert.ToString(dr["name"]);
                        tenantLog.RequestedDate = dr["Genre"] == System.DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["requested_date"]);
                        tenantLog.ResolveDate = dr["resolve_date"] == System.DBNull.Value ? DateTime.Now : Convert.ToDateTime(dr["resolve_date"]);
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
        public int UpdateLogStatus(LogStatus logStatus)
        {
            int logId = 0;
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("usp_savecategory", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new NpgsqlParameter("@LogId", logStatus.LogId));
                    cmd.Parameters.Add(new NpgsqlParameter("@Status", logStatus.Status));
                    cmd.Parameters.Add(new NpgsqlParameter("@AssignedTo", logStatus.AssignedTo));
                    cmd.ExecuteNonQuery();
                    logId = logStatus.LogId;
                    cmd.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return logId;
        }
        public int SaveTenantLog(TenantLog tenantLog)
        {
            int logId = 0;
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BizBuildingDB"].ToString();
                    connection.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("usp_savetenantlog", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new NpgsqlParameter("@CategoryId", tenantLog.CategoryId));
                    cmd.Parameters.Add(new NpgsqlParameter("@Description", tenantLog.Description));
                    cmd.Parameters.Add(new NpgsqlParameter("@Location", tenantLog.Location));
                    logId = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            return logId;
        }        
    }
}
