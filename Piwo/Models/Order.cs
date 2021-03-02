using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Piwo.Models
{
    public class Order
    {
        public Order()
        {
            OrdersDetails = new List<OrdedDetails>();
        }
        public int Id { get; set; }
        

        [Display(Name="Numer zamówienia")]
        public string OrderNumber { get; set; }
        [Required]
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }
        
        [Required]
        [Display(Name ="Imie i nazwisko")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name="Adres")]
        public string Adress { get; set; }

        [Display(Name="Data zamówienia")]
        public string OrderDate { get; set; }

        public virtual List<OrdedDetails> OrdersDetails { get; set; }
    }
}
