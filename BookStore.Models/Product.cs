using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, MaxLength(100), Display(Name = "Titel")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Omschrijving")]
        public string? Description { get; set; }

        [Required, RegularExpression(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$")]
        public string ISBN { get; set; } = string.Empty;

        [Required, MaxLength(100), Display(Name = "Schrijver")]
        public string Author { get; set; } = string.Empty;

        [Required, Display(Name = "Catalogusprijs")]
        public double ListPrice { get; set; }

        [Required, Display(Name = "Prijs")]
        public double Price { get; set; }

        [Required, Display(Name = "Prijs bij 50+ afname")]
        public double Price50 { get; set; }

        [Required, Display(Name = "Prijs bij 100+ afname")]
        public double Price100 { get; set; }

        public string? ImageUrl { get; set; }

        [Required , Display(Name="Categorie")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [Required , Display(Name = "Soort Kaft")]
        public int CoverTypeId { get; set; }
        public CoverType? CoverType { get; set; }
    }
}
