using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CampusEventHubApi.Data;
using CampusEventHubApi.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace CampusEventHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.User.ToListAsync();

            return Ok(users); // Return all users as an array
        }

        // POST: api/Users/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest("Nema user date.");
            }

            // provjera jeli username ili email vec postoji
            if (_context.User.Any(u => u.Username == newUser.Username || u.Email == newUser.Email))
            {
                return BadRequest("Username ili email vec postoji.");
            }

            // hashiraj password
            newUser.PasswordHash = HashPassword(newUser.PasswordHash);

            // dodat u databazu
            _context.User.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Register), new { id = newUser.IDUser }, newUser);
        }

        // POST: api/Users/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Nema login date.");
            }

            var user = await _context.User.SingleOrDefaultAsync(u => u.Username == loginRequest.Username);

            if (user == null || !VerifyPassword(loginRequest.Password, user.PasswordHash))
            {
                return Unauthorized("Kriv username ili password.");
            }

            // login uspjesan!!!!!!!!!!
            return Ok(new { user.IDUser, user.Username, user.IsAdmin });
        }

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (updatedUser == null)
            {
                return BadRequest("User data is required.");
            }

            // Check if the user exists
            var existingUser = await _context.User.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound(); // Return 404 if the user does not exist
            }

            // Optionally, you can check if the username or email is already taken by another user
            if (_context.User.Any(u => (u.Username == updatedUser.Username || u.Email == updatedUser.Email) && u.IDUser != id))
            {
                return BadRequest("Username or email is already taken.");
            }

            // Update the user's fields
            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;
            existingUser.PasswordHash = updatedUser.PasswordHash; // You can hash the password again if it's updated
            existingUser.IsAdmin = updatedUser.IsAdmin;
            existingUser.ImageUrl = updatedUser.ImageUrl;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 No Content if the update is successful
        }



        // DELETE: api/Users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound(); // Return 404 if user not found
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent(); // Return 204 No Content (successful deletion)
        }

        // hashiranje i saltiranje passworda (HMACSHA512)
        private string HashPassword(string password)
        {
            // generiranje salta od 16 byteova
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // hashiraj password sa salton koristeci HMACSHA512
            using (var hmac = new HMACSHA512(salt))
            {
                var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                // kombinacija hasha i salta
                byte[] hashBytes = new byte[salt.Length + hash.Length];
                Buffer.BlockCopy(salt, 0, hashBytes, 0, salt.Length);
                Buffer.BlockCopy(hash, 0, hashBytes, salt.Length, hash.Length);

                return Convert.ToBase64String(hashBytes);
            }
        }

        // verifikacija passworda
        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            // dekodira spremljeni hash
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            // ekstracta salt tj prvih 16 bytova
            byte[] salt = new byte[16];
            Buffer.BlockCopy(hashBytes, 0, salt, 0, salt.Length);

            // ekstrakta hash sta je ostatak bytova
            byte[] storedPasswordHash = new byte[hashBytes.Length - salt.Length];
            Buffer.BlockCopy(hashBytes, salt.Length, storedPasswordHash, 0, storedPasswordHash.Length);

            // hashiraj inputirani password sa ekstraktanin salton
            using (var hmac = new HMACSHA512(salt))
            {
                var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputPassword));

                // kompariraj generirani hash sa vec postojcim hashen
                return hash.SequenceEqual(storedPasswordHash);
            }
        }
    }

    // data transfer objekt za login request
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}