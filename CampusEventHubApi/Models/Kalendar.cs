namespace CampusEventHubApi.Models
{
    public class Kalendar
    {
        public int KalendarID { get; set; } // Jedinstveni identifikator događaja
        public string Naslov { get; set; } // Naslov događaja
        public string Opis { get; set; } // Opis događaja (opcionalno)
        public DateTime Datum { get; set; } // Datum događaja
        public TimeSpan? Vrijeme { get; set; } // Vrijeme događaja (opcionalno)
        public DateTime Kreirano { get; set; } = DateTime.Now; // Vrijeme kada je bilješka kreirana
    }
}
