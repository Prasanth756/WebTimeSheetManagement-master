using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTimeSheetManagement.Models
{
    [Table("Registration")]
        public class Registration
        {
            [Key]
            public int RegistrationID { get; set; }

            [Required(ErrorMessage = "Enter Name")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Mobileno Required")]
            [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong Mobileno")]
            public string Mobileno { get; set; }

            [Required(ErrorMessage = "EmailID Required")]
            [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Please enter a valid e-mail adress")]
            public string EmailID { get; set; }

            [MinLength(6, ErrorMessage = "Minimum Username must be 6 in charaters")]
            [Required(ErrorMessage = "Username Required")]
            public string Username { get; set; }

            [MinLength(7, ErrorMessage = "Minimum Password must be 7 in charaters")]
            [Required(ErrorMessage = "Password Required")]
            public string Password { get; set; }

            [Compare("Password", ErrorMessage = "Enter Valid Password")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "Gender Required")]
            public string Gender { get; set; }

            public DateTime? Birthdate { get; set; }

            public DateTime? DateofJoining { get; set; }

            public int? RoleID { get; set; }

        public List<RolesNew> ListofRoles { get; set; }

        //[MaxLength(4, ErrorMessage = "Minimum Password must be 6 in charaters")]
        public string EmployeeID { get; set; }

            public DateTime? CreatedOn { get; set; }

            public int? ForceChangePassword { get; set; }

       
            [Required(ErrorMessage ="AadharNumber Required ")]
            //[RegularExpression(@"/^[2-9]{1}[0-9]{3}[\s][0-9]{4[\s][0-9]{4}$/", ErrorMessage = "Enter Valid AadharNumber")]
            [MinLength(12, ErrorMessage = "Minimum must be 12 in charaters")]
            public string AadharNumber { get; set; }

            [Required(ErrorMessage ="PanNumber Required")]
            public string PanCardNumber { get; set; }

            public int? Designation { get; set; }

            public List<Designations> ListofDesignations { get; set; }
           //    public int DesignationId { get; set; }

    }
    //[NotMapped]
    public class Designations
    {
        [Key]
        public string DesignationId { get; set; }

        public string EmpDesignation { get; set; }

    }

    //Created By Prasanth

    public class RolesNew
    {
        [Key]
        public String RoleID { get; set; }

        public string Rolename { get; set; }
    }



}
