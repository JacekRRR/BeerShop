﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Piwo.Areas.Admin.Models
{
    public class RoleUserVm
    {
        [Required]
        [DisplayName("Użytkownik")]
        public string UserId { get; set; }

        public string RoleId { get; set; }
    }
}
