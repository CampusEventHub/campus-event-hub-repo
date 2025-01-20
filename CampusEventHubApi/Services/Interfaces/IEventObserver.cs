using CampusEventHubApi.Models;

namespace CampusEventHubApi.Services.Interfaces
{
    public interface IEventObserver
    {
        void OnEventCreated(Kalendar kalendar);
        void OnEventUpdated(Kalendar kalendar);
        void OnEventDeleted(int kalendarId);
    }
}
