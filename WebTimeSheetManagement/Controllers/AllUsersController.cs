using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTimeSheetManagement.Concrete;
using WebTimeSheetManagement.Filters;
using WebTimeSheetManagement.Interface;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Controllers
{
    [ValidateSuperAdminSession]

    public class AllUsersController : Controller
    {
        private IUsers _IUsers;
        private IRegistration _IRegistration;
        private IRoles _IRoles;

        public AllUsersController()
        {
            _IUsers = new UsersConcrete();
            _IRegistration = new RegistrationConcrete();
            _IRoles = new RolesConcrete();
        }

        // GET: AllUsers
        public ActionResult Users()
        {
            return View();
        }

        public ActionResult LoadUsersData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                var rolesData = _IUsers.ShowallUsers(sortColumn, sortColumnDir, searchValue);
                recordsTotal = rolesData.Count();
                var data = rolesData.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult UserDetails(int? RegistrationID)
        {
            try
            {
                if (RegistrationID == null)
                {

                }
                var userDetailsResponse = _IUsers.GetUserDetailsByRegistrationID(RegistrationID);
                return PartialView("_UserDetails", userDetailsResponse);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Added By Prasanth
        //Edit the User Details

        public ActionResult EditDetails(int? RegistrationID)
        {
            Registration reg = new Registration();
            //reg.ListofDesignations = _IRegistration.ListofDesignations();
            //reg.ListofRoles = _IRegistration.ListofRoles();

            reg.ListofDesignations = _IRegistration.ListofDesignations();
            reg.ListofRoles = _IRegistration.ListofRoles();

            ViewBag.ListofRoles = reg.ListofRoles;
            ViewBag.ListofDesignations = reg.ListofDesignations;

            try
            {
                if (RegistrationID == null)
                {

                }
                var userDetailsResponse = _IUsers.GetEditableUserDetailsByRegistrationID(RegistrationID);
                return PartialView("_EditUserDetails", userDetailsResponse);
            }
            catch (Exception)
            {
                throw;
            }
        }

        ////Created By Prasanth For Save Edit Details
        [HttpPost]
        public Registration SaveEditDetails(Registration registration)
        {
           
            try
            {
                if (ModelState.IsValid)
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        db.Entry(registration).State = EntityState.Modified;
                        db.SaveChanges();
                        
                    }
                }
                //else
                //{
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return registration;
        }

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult LoadAdminsData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                var rolesData = _IUsers.ShowallAdmin(sortColumn, sortColumnDir, searchValue);
                recordsTotal = rolesData.Count();
                var data = rolesData.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult AdminDetails(int? RegistrationID)
        {
            try
            {
                if (RegistrationID == null)
                {

                }
                var userDetailsResponse = _IUsers.GetAdminDetailsByRegistrationID(RegistrationID);
                return PartialView("_UserDetails", userDetailsResponse);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}