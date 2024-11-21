using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class CoverType
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [MaxLength(50, ErrorMessage = "{0} mag maximaal {1} teken bevatten")]
        [Display(Name = "Soort Kaft")]
        public string? Name { get; set; }
    }
}
