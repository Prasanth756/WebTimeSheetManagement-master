using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Interface
{
    public interface IAssignRoles
    {
        List<AdminModel> ListofAdmins();
        List<UserModel> ListofUser();
        int UpdateAssigntoAdmin(string AssignToAdminID, string UserID);
        IQueryable<UserModel> ShowallRoles(string sortColumn, string sortColumnDir, string Search);
        bool RemovefromUserRole(string RegistrationID);
        List<UserModel> GetListofUnAssignedUsers();
        bool SaveAssignedRoles(AssignRolesModel AssignRolesModel);
        bool CheckIsUserAssignedRole(int RegistrationID);
        //created by shiva for list of projects
        List<ProjectModel> ListOfProjects();

        //Created by Shiva for AssignProject date:01/11/2022
        bool SaveAssignProjects(AssignedProjectsModel assignedProjectsModel);

        //created by shiva for list of  users and projets under admin
        // List<AdminModel> ListofUserofAdmin(int adminid);
        List<UserAdminModel> ListofUserofAdmin(int adminid);
        // List<ProjectModel> ListOfProjectsOfAdmin()
        List<ProjectModel> GetListofProjects(string userid);
    }
}
