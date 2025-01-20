using System.ComponentModel.DataAnnotations;

namespace CampusEventHubApi.DTOs
{
    public class OcjenaRequestDto
    {
        [Required(ErrorMessage = "ID događaja je obavezan.")]
        public int EventID { get; set; }       // ID događaja (strani ključ)

        [Required(ErrorMessage = "ID korisnika je obavezan.")]
        public int UserID { get; set; }        // ID korisnika (strani ključ)

        [Required(ErrorMessage = "Ocjena je obavezna.")]
        [Range(1, 5, ErrorMessage = "Ocjena mora biti između 1 i 5.")]
        public int OcjenaVrijednost { get; set; } // Ocjena (1-5)

        [MaxLength(1000, ErrorMessage = "Komentar može imati najviše 1000 znakova.")]
        public string? Komentar { get; set; }  // Opcionalni komentar korisnika
    }
}
