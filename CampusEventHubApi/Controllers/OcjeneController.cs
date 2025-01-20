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
    public class OcjeneController : ControllerBase
    {
        private readonly IOcjenaService _ocjenaService;

        public OcjeneController(IOcjenaService ocjenaService)
        {
            _ocjenaService = ocjenaService;
        }

        // GET: api/Ocjene
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OcjenaResponseDto>>> GetOcjene()
        {
            var ocjene = await _ocjenaService.GetAllAsync();
            return Ok(ocjene);
        }

        // GET: api/Ocjene/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OcjenaResponseDto>> GetOcjena(int id)
        {
            var ocjena = await _ocjenaService.GetByIdAsync(id);

            if (ocjena == null)
            {
                return NotFound("Ocjena nije pronađena.");
            }

            return Ok(ocjena);
        }

        // POST: api/Ocjene
        [HttpPost]
        public async Task<ActionResult<OcjenaResponseDto>> CreateOcjena([FromBody] OcjenaRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdOcjena = await _ocjenaService.CreateAsync(request);
                return CreatedAtAction(nameof(GetOcjena), new { id = createdOcjena.OcjenaID }, createdOcjena);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Ocjene/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOcjena(int id, [FromBody] OcjenaUpdateDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = await _ocjenaService.UpdateAsync(id, request);

            if (!isUpdated)
            {
                return NotFound("Ocjena nije pronađena.");
            }

            return NoContent();
        }

        // DELETE: api/Ocjene/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOcjena(int id)
        {
            var isDeleted = await _ocjenaService.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound("Ocjena nije pronađena.");
            }

            return NoContent();
        }
    }
}
