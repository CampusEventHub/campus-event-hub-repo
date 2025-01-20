using CampusEventHubApi.DTOs;
using CampusEventHubApi.Models;

namespace CampusEventHubApi.Factories
{
    public static class OcjenaDtoFactory
    {
        // Kreiranje Response DTO-a
        public static OcjenaResponseDto CreateResponseDto(Ocjena ocjena)
        {
            return new OcjenaResponseDto
            {
                OcjenaID = ocjena.OcjenaID,
                EventID = ocjena.EventID,
                EventNaslov = ocjena.Event?.Title, // Provjera null vrijednosti
                UserID = ocjena.UserID,
                Username = ocjena.User?.Username, // Provjera null vrijednosti
                OcjenaVrijednost = ocjena.OcjenaVrijednost,
                Komentar = ocjena.Komentar,
                Kreirano = ocjena.Kreirano
            };
        }

        // Kreiranje Request DTO-a
        public static OcjenaRequestDto CreateRequestDto(Ocjena ocjena)
        {
            return new OcjenaRequestDto
            {
                EventID = ocjena.EventID,
                UserID = ocjena.UserID,
                OcjenaVrijednost = ocjena.OcjenaVrijednost,
                Komentar = ocjena.Komentar
            };
        }

        // Kreiranje Update DTO-a
        public static OcjenaUpdateDto CreateUpdateDto(Ocjena ocjena)
        {
            return new OcjenaUpdateDto
            {
                OcjenaVrijednost = ocjena.OcjenaVrijednost,
                Komentar = ocjena.Komentar
            };
        }
    }
}
