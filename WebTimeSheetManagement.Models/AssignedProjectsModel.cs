using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTimeSheetManagement.Models
{
   public class AssignedProjectsModel
    {
        public List<AdminModel> ListofAdmins { get; set; }

        [Required(ErrorMessage = "Choose Admin")]
        public int RegistrationID { get; set; }

        public List<ProjectModel> ListOfProjects{ get; set; }
        public string ProjectID { get; set; }
        public int? CreatedBy { get; set; }

        //created by shiva  for  listofusers
        //added for user selection by shiva
        public List<UserAdminModel> ListofusersunderAdmin { get; set; }

        [Required(ErrorMessage = "Choose user")]
        public int UserRegistraionId { get; set; }

    }

    [NotMapped]
    public class ProjectModel
    {
        public string ProjectID { get; set; }
        public string ProjectName { get; set; }

        //created by shiva for  check box for projects
        public int RegistrationID { get; set; }
        public string Name { get; set; }
        public bool SelectedProject { get; set; }
        public string Assigntoproject { get; set; }
    }

    [NotMapped]
    public class UserAdminModel
    {
        // public string AssignToAdmin { get; set; }
        public string RegistrationID { get; set; }
        public string Name { get; set; }
    }
}
