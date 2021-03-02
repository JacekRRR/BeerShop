using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Piwo.Models
{
    public class Categories
    {
        public int Id { get; set; }

        [Required (ErrorMessage ="Pole nie może być puste")]
        [Display(Name ="Rodzaje produktów")]
        public string TypeOfProduct { get; set; }
    }
}
