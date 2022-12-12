using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTimeSheetManagement.Models
{
    [Table("ProjectMaster")]
    public class ProjectMaster
    {
        [Key]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Enter Project Code")]
        [MinLength(1, ErrorMessage = "Minimum Project Code must be 1 in charaters")]
        public string ProjectCode { get; set; }

        [Required(ErrorMessage = "Enter Nature of Industry")]
        [MinLength(1, ErrorMessage = "Minimum Nature of Industry must be 14 in charaters")]
        public string NatureofIndustry { get; set; }

        [Required(ErrorMessage = "Enter ProjectName")]
        [MinLength(1, ErrorMessage = "Minimum ProjectName must be 6 in charaters")]
        public string ProjectName { get; set; }
    }
}
