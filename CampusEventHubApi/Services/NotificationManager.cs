namespace CampusEventHubApi.Services
{
    public class NotificationManager
    {
        private static NotificationManager _instance;
        private static readonly object _lock = new object();

        // Privatni konstruktor sprječava direktno stvaranje instanci
        private NotificationManager() { }

        // Metoda za dohvat jedinstvene instance
        public static NotificationManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new NotificationManager();
                    }
                    return _instance;
                }
            }
        }

        public void Notify(string message)
        {
            // Logika za slanje notifikacija
            Console.WriteLine($"Notifikacija: {message}");
        }
    }
}
