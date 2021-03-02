using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Piwo.Models
{
    public class AppUser:IdentityUser
    {
        [Display(Name="Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
    }
}
