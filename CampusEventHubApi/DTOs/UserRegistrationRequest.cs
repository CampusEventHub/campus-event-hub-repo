using System.ComponentModel.DataAnnotations;

namespace CampusEventHubApi.DTOs
{
    public class UserRegistrationRequest
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
