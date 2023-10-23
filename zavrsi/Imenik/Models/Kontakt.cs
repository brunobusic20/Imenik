using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImenikApp.Models
{
    public class Kontakt : Entitet
    {
        [Required]
        public string? Ime { get; set; }
        public string? Prezime { get; set; }
        public string? Broj { get; set; }
        public string? Adresa { get; set; }
        
    }
}