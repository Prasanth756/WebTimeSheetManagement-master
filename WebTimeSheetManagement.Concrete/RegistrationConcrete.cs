using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Interface;
using WebTimeSheetManagement.Models;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using Dapper;
using System.Configuration;

namespace WebTimeSheetManagement.Concrete
{
    public class RegistrationConcrete : IRegistration
    {
        public bool CheckUserNameExists(string Username)
             {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var result = (from user in _context.Registration
                                  where user.Username == Username
                                  select user).Count();

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckMobileNumberExists(string Mobileno)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var result = (from mobile in _context.Registration
                                  where mobile.Mobileno == Mobileno
                                  select mobile).Count();

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckAadharExists(string AadharNumber)
        {

            try
            {
                using (var _context = new DatabaseContext())
                {
                    var result = (from Aadhar in _context.Registration
                                  where Aadhar.AadharNumber == AadharNumber
                                  select Aadhar).Count();

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool CheckPanCardExists(string PanCardNumber)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var result = (from PanCard in _context.Registration
                                  where PanCard.PanCardNumber == PanCardNumber
                                  select PanCard).Count();

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckUNameExists(string UName)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var result = (from user in _context.UserInfoes
                                  where user.UName == UName
                                  select user).Count();

                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int AddUser(Registration entity)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    _context.Registration.Add(entity);
                    return _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<Registration> ListofRegisteredUser(string sortColumn, string sortColumnDir, string Search)
        {
            try
            {
                var _context = new DatabaseContext();

                var IQueryableRegistered = (from register in _context.Registration
                                            select register
                                );

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    IQueryableRegistered = IQueryableRegistered.OrderBy(sortColumn + " " + sortColumnDir);
                }
                if (!string.IsNullOrEmpty(Search))
                {
                    IQueryableRegistered = IQueryableRegistered.Where(m => m.Username.Contains(Search) || m.Name.Contains(Search) ||m.EmailID.Contains(Search));
                }

                return IQueryableRegistered;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdatePassword(string RegistrationID,string Password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
                {
                    var param = new DynamicParameters();
                    param.Add("@RegistrationID", RegistrationID);
                    param.Add("@Password", Password);
                    var result = con.Execute("Usp_UpdatePasswordbyRegistrationID", param, null, 0, System.Data.CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        return true; 
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //created by prasanth
        public List<Designations> ListofDesignations()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();
                try
                {
                    var result = con.Query<Designations>("GetDesignations", null, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
                   result.Insert(0, new Designations { EmpDesignation = "----Select----", DesignationId = "" });
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }


        }

        //Created By Prasanth

        public List<RolesNew> ListofRoles()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();
                try
                {
                    var result = con.Query<RolesNew>("GetRoles", null, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
                    result.Insert(0, new RolesNew { Rolename = "----Select----", RoleID = "" });
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

    }
}
