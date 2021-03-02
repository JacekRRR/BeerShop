using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Piwo.Areas.Admin.Models
{
    public class UserRoleMaping
    {
        public string UserId { get; set; }
        
        [Required]
        [Display(Name ="Role")]
        public string RoleId { get; set; }

        public string UserName { get; set; }

        public string RoleName { get; set; }
    }
}
