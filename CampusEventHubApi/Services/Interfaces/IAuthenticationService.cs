using CampusEventHubApi.Models;
using System.Threading.Tasks;

namespace CampusEventHubApi.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> RegisterUserAsync(string username, string email, string password);
        Task<User> AuthenticateUserAsync(string username, string password);
    }
}
