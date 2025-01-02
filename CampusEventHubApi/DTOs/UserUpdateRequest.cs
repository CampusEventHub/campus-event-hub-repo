using System.ComponentModel.DataAnnotations;

namespace CampusEventHubApi.DTOs
{
    public class UserUpdateRequest
    {
        [MaxLength(50)]
        public string Username { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [MinLength(6)]
        public string Password { get; set; }
    }
}
