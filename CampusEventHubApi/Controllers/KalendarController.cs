using CampusEventHubApi.DTOs;
using CampusEventHubApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusEventHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KalendarController : ControllerBase
    {
        private readonly IKalendarService _kalendarService;
        private const string NotFoundMessage = "Događaj nije pronađen.";

        public KalendarController(IKalendarService kalendarService)
        {
            _kalendarService = kalendarService;
        }

        // GET: api/Kalendar
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<KalendarRequestDto>), 200)]
        public async Task<ActionResult<IEnumerable<KalendarRequestDto>>> GetKalendari()
        {
            var kalendari = await _kalendarService.GetAllAsync();
            return Ok(kalendari);
        }

        // GET: api/Kalendar/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(KalendarRequestDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<KalendarRequestDto>> GetKalendar(int id)
        {
            var kalendar = await _kalendarService.GetByIdAsync(id);

            if (kalendar == null)
            {
                return NotFound(NotFoundMessage);
            }

            return Ok(kalendar);
        }

        // POST: api/Kalendar
        [HttpPost]
        [ProducesResponseType(typeof(KalendarRequestDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<KalendarRequestDto>> CreateKalendar([FromBody] KalendarRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdKalendar = await _kalendarService.CreateAsync(request);
                return CreatedAtAction(nameof(GetKalendar), new { id = createdKalendar.Naslov }, createdKalendar);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message); // Vraćanje poruke o grešci iz servisa
            }
        }

        // PUT: api/Kalendar/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateKalendar(int id, [FromBody] KalendarRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var isUpdated = await _kalendarService.UpdateAsync(id, request);

                if (!isUpdated)
                {
                    return NotFound(NotFoundMessage);
                }

                return NoContent();
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message); // Vraćanje poruke o grešci iz servisa
            }
        }

        // GET: api/Kalendar/month/{month}/{year}
        [HttpGet("month/{month}/{year}")]
        [ProducesResponseType(typeof(IEnumerable<KalendarRequestDto>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<KalendarRequestDto>>> GetKalendariByMonth(int month, int year)
        {
            var kalendari = await _kalendarService.GetByMonthAsync(month, year);

            if (kalendari == null || !kalendari.Any())
            {
                return NotFound("No events available for this month.");
            }

            return Ok(kalendari);
        }


        // DELETE: api/Kalendar/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteKalendar(int id)
        {
            var isDeleted = await _kalendarService.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound(NotFoundMessage);
            }

            return NoContent();
        }
    }
}
