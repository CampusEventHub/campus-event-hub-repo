using System.ComponentModel.DataAnnotations;

namespace CampusEventHubApi.DTOs
{
    public class LoginRequest
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
