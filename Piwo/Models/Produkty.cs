using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Piwo.Models
{
    public class Produkty
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name="Nazwa")]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Browar")]
        public string Brewery { get; set; }

        [Required]
        [Display(Name ="Zawartość alkocholu")]
        public float alcoholContent { get; set; }

       
        
        [Required]
        [Display(Name ="Dostępność")]
        public bool IsAvalible { get; set; }

        [Display(Name ="Dostępne sztuki")]
        public int? NumberOfItems { get; set; }

        [Display(Name ="Id Kategorii")]
        [ForeignKey("CategoryId")]
        public int CategoriesId { get; set; }
        

        
        [Display(Name="Kategoria")]
        public Categories Category { get; set; }
        [ForeignKey("specialTagsId")]
        public int SpecialTagId { get; set; }
        

        public SpecialTags specialTags { get; set; }

        [Display(Name ="Cena")]
        public decimal Price { get; set; }

        [Display(Name ="Wybierz zdjęcie")]
        public string Image { get; set; }
    }
}
