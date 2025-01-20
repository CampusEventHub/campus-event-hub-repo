using System.ComponentModel.DataAnnotations;

namespace CampusEventHubApi.DTOs
{
    public class OcjenaUpdateDto
    {
        [Required(ErrorMessage = "Ocjena je obavezna.")]
        [Range(1, 5, ErrorMessage = "Ocjena mora biti između 1 i 5.")]
        public int OcjenaVrijednost { get; set; } // Ocjena (1-5)

        [MaxLength(1000, ErrorMessage = "Komentar može imati najviše 1000 znakova.")]
        public string? Komentar { get; set; }    // Opcionalni komentar korisnika
    }
}
