using System;
using System.ComponentModel.DataAnnotations;

namespace CampusEventHubApi.DTOs
{
    public class KalendarResponseDto
    {
        [Required(ErrorMessage = "KalendarID je obavezan.")]
        public int KalendarID { get; set; } // Jedinstveni identifikator događaja

        [Required(ErrorMessage = "Naslov je obavezan.")]
        [MaxLength(255, ErrorMessage = "Naslov može imati najviše 255 znakova.")]
        public string Naslov { get; set; } // Naslov događaja

        [MaxLength(1000, ErrorMessage = "Opis može imati najviše 1000 znakova.")]
        public string? Opis { get; set; } // Opis događaja (opcionalno)

        [Required(ErrorMessage = "Datum je obavezan.")]
        [RegularExpression(@"\d{2}\.\d{2}\.\d{4}", ErrorMessage = "Datum mora biti u formatu dd.MM.yyyy.")]
        public string Datum { get; set; } // Datum događaja (formatirano kao string dd.MM.yyyy)

        [RegularExpression(@"([01]?[0-9]|2[0-3]):[0-5][0-9]", ErrorMessage = "Vrijeme mora biti u formatu HH:mm.")]
        public string? Vrijeme { get; set; } // Vrijeme događaja (formatirano kao string HH:mm, opcionalno)

        [Required(ErrorMessage = "Vrijeme kreiranja je obavezno.")]
        [RegularExpression(@"\d{2}\.\d{2}\.\d{4} \d{2}:\d{2}:\d{2}", ErrorMessage = "Vrijeme kreiranja mora biti u formatu dd.MM.yyyy HH:mm:ss.")]
        public string Kreirano { get; set; } // Vrijeme kreiranja događaja (formatirano kao string dd.MM.yyyy HH:mm:ss)
    }
}
