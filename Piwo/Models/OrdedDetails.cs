using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Piwo.Models
{
    public class OrdedDetails
    {
        public int Id { get; set; }

        [Display(Name ="Zamówienie")]
        public int OrderId { get; set;}

        [Display(Name ="Produkt")]
        public int ProductId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("ProductId")]
        public Produkty Product { get; set; }
    }
}
