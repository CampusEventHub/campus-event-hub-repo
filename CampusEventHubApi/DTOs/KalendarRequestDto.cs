using System;
using System.ComponentModel.DataAnnotations;

namespace CampusEventHubApi.DTOs
{
    public class KalendarRequestDto
    {
        [Required(ErrorMessage = "Naslov je obavezan.")]
        [MaxLength(255, ErrorMessage = "Naslov može imati najviše 255 znakova.")]
        public string Naslov { get; set; } // Naslov događaja

        [MaxLength(1000, ErrorMessage = "Opis može imati najviše 1000 znakova.")]
        public string? Opis { get; set; } // Opis događaja (opcionalno)

        [Required(ErrorMessage = "Datum je obavezan.")]
        [RegularExpression(@"^\d{2}\.\d{2}\.\d{4}$", ErrorMessage = "Datum mora biti u formatu dd.MM.yyyy.")]
        public string Datum { get; set; } // Datum događaja kao string za validaciju formata

        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Vrijeme mora biti u formatu HH:mm.")]
        public string? Vrijeme { get; set; } // Vrijeme događaja (opcionalno)
    }
}
