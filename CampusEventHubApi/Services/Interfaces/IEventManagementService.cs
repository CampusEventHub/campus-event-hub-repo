using CampusEventHubApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusEventHubApi.Services.Interfaces
{
    public interface IEventManagementService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(int id);
        Task<bool> CreateEventAsync(Event newEvent);
        Task<bool> UpdateEventAsync(int id, Event updatedEvent);
        Task<bool> DeleteEventAsync(int id);
    }
}
