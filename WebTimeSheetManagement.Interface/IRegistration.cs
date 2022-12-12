using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Interface
{
    public interface IRegistration
    {
        bool CheckUserNameExists(string Username);

        bool CheckMobileNumberExists(string Mobileno);

        bool CheckAadharExists(string AadharNumber);

        bool CheckPanCardExists(string PanCardNumber);

        int AddUser(Registration entity);

        IQueryable<Registration> ListofRegisteredUser(string sortColumn, string sortColumnDir, string Search);

        bool UpdatePassword(string RegistrationID, string Password);

        // int AddUser(UserInfo userInfo);
        bool CheckUNameExists(string UName);

        //Created By Prasanth

        List<Designations> ListofDesignations();

        List<RolesNew> ListofRoles();
    }
}
