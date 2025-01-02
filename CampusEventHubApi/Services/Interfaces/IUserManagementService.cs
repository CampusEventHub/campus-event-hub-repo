using CampusEventHubApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusEventHubApi.Services.Interfaces
{
    public interface IUserManagementService
    {
        // Upravljanje korisnicima
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(int id, User updatedUser);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
    }
}
