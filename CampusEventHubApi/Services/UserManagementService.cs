using CampusEventHubApi.Data;
using CampusEventHubApi.Models;
using CampusEventHubApi.Security;
using CampusEventHubApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusEventHubApi.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly ApplicationDbContext _context;

        public UserManagementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.User.FindAsync(id);
        }

        public async Task<bool> UpdateUserAsync(int id, User updatedUser)
        {
            var existingUser = await _context.User.FindAsync(id);
            if (existingUser == null)
                return false;

            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;
            existingUser.PasswordHash = updatedUser.PasswordHash;
            existingUser.Salt = updatedUser.Salt;

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
                return false;

            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            var user = await _context.User.FindAsync(userId);
            if (user == null || !PasswordHashProvider.VerifyPassword(oldPassword, user.PasswordHash, user.Salt))
                return false;

            var newSalt = PasswordHashProvider.GetSalt();
            var newHashedPassword = PasswordHashProvider.GetHash(newPassword, newSalt);

            user.PasswordHash = newHashedPassword;
            user.Salt = newSalt;

            _context.User.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
