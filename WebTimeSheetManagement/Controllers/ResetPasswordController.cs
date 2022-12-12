using EventApplicationCore.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTimeSheetManagement.Concrete;
using WebTimeSheetManagement.Filters;
using WebTimeSheetManagement.Interface;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace WebTimeSheetManagement.Controllers
{
    [ValidateSuperAdminSession]
    public class ResetPasswordController : Controller
    {
        IRegistration _IRegistration;
        public ResetPasswordController()
        {
            _IRegistration = new RegistrationConcrete();
        }

        // GET: ResetPassword
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadRegisteredUserData()
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

                var projectdata = _IRegistration.ListofRegisteredUser(sortColumn, sortColumnDir, searchValue);
                recordsTotal = projectdata.Count();
                var data = projectdata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }

        public JsonResult ResetUserPasswordProcess(string RegistrationID)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(RegistrationID)))
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

                var Password = EncryptionLibrary.EncryptText("default@123");
                var isPasswordUpdated = _IRegistration.UpdatePassword(RegistrationID, Password);

                if (isPasswordUpdated)
                {
                    return Json(data: true, behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(data: false, behavior: JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public JsonResult SendMailToUser()
        //{
        //    bool result = false;
        //    result = SendEmail("tejasurya520@gmail.com", "Sagar Email sending test"," <p>Hi Sagar,<br/>we have reseted your password.<br/>Regards SuperAdmin</p>");
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //public bool SendEmail(string toEmail,string subject,string emailBody)
        //{
        //    try
        //    {
        //        string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
        //        string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

        //        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        //        client.EnableSsl = true;
        //        client.Timeout = 100000;
        //        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        client.UseDefaultCredentials = false;
        //        client.Credentials = new NetworkCredential(senderEmail, senderPassword);

        //        MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, emailBody);
        //        mailMessage.IsBodyHtml = true;
        //        mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
        //        client.Send(mailMessage);

        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        return false;
        //    }
        //}


    }
}