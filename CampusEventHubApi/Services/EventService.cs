using CampusEventHubApi.Data;
using CampusEventHubApi.Models;
using CampusEventHubApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusEventHubApi.Services
{
    public class EventService : IEventManagementService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Event.ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _context.Event.FindAsync(id);
        }

        public async Task<bool> CreateEventAsync(Event newEvent)
        {
            var user = await _context.User.FindAsync(newEvent.UserID);
            if (user == null)
                return false;

            newEvent.CreatedAt = DateTime.UtcNow;
            _context.Event.Add(newEvent);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateEventAsync(int id, Event updatedEvent)
        {
            var existingEvent = await _context.Event.FindAsync(id);
            if (existingEvent == null)
                return false;

            existingEvent.Title = updatedEvent.Title;
            existingEvent.Description = updatedEvent.Description;
            existingEvent.Location = updatedEvent.Location;
            existingEvent.StartDate = updatedEvent.StartDate;
            existingEvent.EndDate = updatedEvent.EndDate;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var eventItem = await _context.Event.FindAsync(id);
            if (eventItem == null)
                return false;

            _context.Event.Remove(eventItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
