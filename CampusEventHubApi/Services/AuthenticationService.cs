using CampusEventHubApi.Data;
using CampusEventHubApi.Models;
using CampusEventHubApi.Security;
using CampusEventHubApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CampusEventHubApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApplicationDbContext _context;

        public AuthenticationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUserAsync(string username, string email, string password)
        {
            if (_context.User.Any(u => u.Username == username || u.Email == email))
                return false;

            var salt = PasswordHashProvider.GetSalt();
            var hashedPassword = PasswordHashProvider.GetHash(password, salt);

            var newUser = new User
            {
                Username = username,
                Email = email,
                PasswordHash = hashedPassword,
                Salt = salt
            };

            _context.User.Add(newUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> AuthenticateUserAsync(string username, string password)
        {
            var user = await _context.User.SingleOrDefaultAsync(u => u.Username == username);
            if (user == null || !PasswordHashProvider.VerifyPassword(password, user.PasswordHash, user.Salt))
                return null;

            return user;
        }
    }
}
