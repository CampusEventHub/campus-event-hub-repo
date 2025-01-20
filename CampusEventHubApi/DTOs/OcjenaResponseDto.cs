using System;
using System.ComponentModel.DataAnnotations;

namespace CampusEventHubApi.DTOs
{
    public class OcjenaResponseDto
    {
        public int OcjenaID { get; set; }          // Jedinstveni ID ocjene

        [Required(ErrorMessage = "ID događaja je obavezan.")]
        public int EventID { get; set; }           // ID događaja

        public string EventNaslov { get; set; }    // Naslov događaja

        [Required(ErrorMessage = "ID korisnika je obavezan.")]
        public int UserID { get; set; }            // ID korisnika

        public string Username { get; set; }       // Ime korisnika koji je dao ocjenu

        [Range(1, 5, ErrorMessage = "Ocjena mora biti između 1 i 5.")]
        public int OcjenaVrijednost { get; set; }  // Vrijednost ocjene (1-5)

        [MaxLength(1000, ErrorMessage = "Komentar može imati najviše 1000 znakova.")]
        public string? Komentar { get; set; }      // Opcionalni komentar

        public DateTime Kreirano { get; set; }     // Vrijeme kada je ocjena stvorena
    }
}
