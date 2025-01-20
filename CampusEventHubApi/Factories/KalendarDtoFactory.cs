using CampusEventHubApi.DTOs;
using CampusEventHubApi.Models;

namespace CampusEventHubApi.Factories
{
    public static class KalendarDtoFactory
    {
        // Kreiranje KalendarRequestDto
        public static KalendarRequestDto CreateRequestDto(Kalendar kalendar)
        {
            return new KalendarRequestDto
            {
                Naslov = kalendar.Naslov,
                Opis = kalendar.Opis,
                Datum = kalendar.Datum.ToString("dd.MM.yyyy"), // Format datuma
                Vrijeme = kalendar.Vrijeme.HasValue ? kalendar.Vrijeme.Value.ToString(@"hh\:mm") : null // Format vremena
            };
        }

        // Kreiranje KalendarResponseDto
        public static KalendarResponseDto CreateResponseDto(Kalendar kalendar)
        {
            return new KalendarResponseDto
            {
                KalendarID = kalendar.KalendarID,
                Naslov = kalendar.Naslov,
                Opis = kalendar.Opis,
                Datum = kalendar.Datum.ToString("dd.MM.yyyy"), // Format datuma
                Vrijeme = kalendar.Vrijeme.HasValue ? kalendar.Vrijeme.Value.ToString(@"hh\:mm") : null, // Format vremena
                Kreirano = kalendar.Kreirano.ToString("dd.MM.yyyy HH:mm:ss") // Format vremena kreiranja
            };
        }
    }
}
