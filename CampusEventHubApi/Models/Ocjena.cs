using System;

namespace CampusEventHubApi.Models
{
    public class Ocjena
    {
        public int OcjenaID { get; set; }       // Jedinstveni identifikator ocjene
        public int EventID { get; set; }        // Poveznica na događaj
        public Event Event { get; set; }        // Navigacijsko svojstvo prema događaju

        public int UserID { get; set; }         // Poveznica na korisnika
        public User User { get; set; }          // Navigacijsko svojstvo prema korisniku

        public int OcjenaVrijednost { get; set; }  // Ocjena (1-5)
        public string? Komentar { get; set; }   // Opcionalni komentar korisnika
        public DateTime Kreirano { get; set; } = DateTime.Now; // Vrijeme kreiranja
    }
}
