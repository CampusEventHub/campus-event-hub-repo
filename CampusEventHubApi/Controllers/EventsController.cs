using CampusEventHubApi.DTOs;
using CampusEventHubApi.Models;
using CampusEventHubApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusEventHubApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventManagementService _eventService;

        public EventsController(IEventManagementService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return Ok(await _eventService.GetAllEventsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var eventItem = await _eventService.GetEventByIdAsync(id);
            if (eventItem == null)
                return NotFound();

            return Ok(eventItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newEvent = new Event
            {
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                UserID = request.UserID
            };

            var result = await _eventService.CreateEventAsync(newEvent);
            if (!result)
                return BadRequest("User not found.");

            return CreatedAtAction(nameof(CreateEvent), new { id = newEvent.IDEvent }, newEvent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedEvent = new Event
            {
                IDEvent = id,
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                UserID = request.UserID
            };

            var result = await _eventService.UpdateEventAsync(id, updatedEvent);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var result = await _eventService.DeleteEventAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
