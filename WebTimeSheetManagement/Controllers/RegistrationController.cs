using EventApplicationCore.Library;
using System;
using System.Collections.Generic;
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
    public class RegistrationController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        private IRegistration _IRegistration;
        private IRoles _IRoles;
        public RegistrationController()
        {
            _IRegistration = new RegistrationConcrete();
            _IRoles = new RolesConcrete();
        }

        // GET: Registration/Create
        public ActionResult Registration()
        {
            
            // return View(new Registration());
            int id = 0;
            
            Registration reg = new Registration();
            reg.ListofDesignations = _IRegistration.ListofDesignations();
            reg.ListofRoles = _IRegistration.ListofRoles();

            var item = db.Registration.OrderByDescending(m => m.RegistrationID).FirstOrDefault();
            if (id != 0)
            {
                reg = db.Registration.Where(x => x.RegistrationID == id).FirstOrDefault<Registration>();
            }
            else if (item == null)
            {
                reg.EmployeeID = "EMP001";
            }
            else
            {
                //D3 means in UserId last 3 digits
                reg.EmployeeID = "EMP" + (Convert.ToInt32(item.EmployeeID.Substring(4, item.EmployeeID.Length - 4)) + 1).ToString("D3");
            }
            return View(reg);
        }

        // POST: Registration/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(Registration registration)
        {
            try
            {
                var isUsernameExists = _IRegistration.CheckUserNameExists(registration.Username);

                //var isMobileNumberExists = _IRegistration.CheckMobileNumberExists(registration.Mobileno);


                if (isUsernameExists /*&& isMobileNumberExists*/)
                {
                    ModelState.AddModelError("", errorMessage: "Username Already Used try unique one!");
                    //ModelState.AddModelError("", errorMessage: "Mobileno Already Used try unique one!");

                }
                else
                {
                    registration.CreatedOn = DateTime.Now;
                   // registration.RoleID = _IRoles.getRolesofUserbyRolename("Users");
                    registration.Password = EncryptionLibrary.EncryptText(registration.Password);
                    registration.ConfirmPassword = EncryptionLibrary.EncryptText(registration.ConfirmPassword);
                    if (_IRegistration.AddUser(registration) > 0)
                    {
                        TempData["MessageRegistration"] = "Data Saved Successfully!";
                        return RedirectToAction("Registration");
                    }
                    else
                    {
                        return View(registration);
                    }
                }
                return RedirectToAction("Registration");
            }
            catch
            {
                return View(registration);
            }
        }
        public JsonResult CheckMobileNumberExists(string Mobileno)
        {
            try
            {
                var isMobileNumberExists = false;

                if (Mobileno != null)
                {
                    isMobileNumberExists = _IRegistration.CheckMobileNumberExists(Mobileno);
                }

                if (isMobileNumberExists)
                {
                    return Json(data: true);
                }
                else
                {
                    return Json(data: false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult CheckAadharExists(string AadharNumber)
        {
            try
            {
                var isAadharExists = false;

                if (AadharNumber != null)
                {
                    isAadharExists = _IRegistration.CheckAadharExists(AadharNumber);
                }

                if (isAadharExists)
                {
                    return Json(data: true);
                }
                else
                {
                    return Json(data: false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult CheckPanCardExists(string PanCardNumber)
        {
            try
            {
                var isPanCardExists = false;

                if (PanCardNumber != null)
                {
                    isPanCardExists = _IRegistration.CheckPanCardExists(PanCardNumber);
                }

                if (isPanCardExists)
                {
                    return Json(data: true);
                }
                else
                {
                    return Json(data: false);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult CheckUserNameExists(string Username)
        {
            try
            {
                var isUsernameExists = false;

                if (Username != null)
                {
                    isUsernameExists = _IRegistration.CheckUserNameExists(Username);
                }

                if (isUsernameExists)
                {
                    return Json(data: true);
                }
                else
                {
                    return Json(data: false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult CheckUNameExists(string UName)
        {
            try
            {
                var isUsernameExists = false;

                if (UName != null)
                {
                    isUsernameExists = _IRegistration.CheckUNameExists(UName);
                }

                if (isUsernameExists)
                {
                    return Json(data: true);
                }
                else
                {
                    return Json(data: false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View(new UserInfo());
        }

        //public ActionResult RegUser()
        //{
        //    EmpRegistrationModel reg = new EmpRegistrationModel();
        //    reg.ListofDesignations = _IRegistration.ListofDesignations();
        //    return View(reg);
        //}
       

    }
}
