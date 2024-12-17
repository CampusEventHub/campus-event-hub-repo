using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CampusEventHubApi.Data;

namespace CampusEventHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase //controllerbase je dobar kad netribaju viewovi ili mvc funkcionalnosti
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("test-connection")]
        public IActionResult TestConnection()
        {
            try
            {
                // database query test konekcije
                var userCount = _context.User.Count();

                if (userCount >= 0)
                {
                    return Ok("Konekcija uspjela!");
                }
                else
                {
                    return StatusCode(500, "Nesupjeh prikupljanja date iz DB.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error konektiranja na DB: {ex.Message}");
            }
        }
    }
}
