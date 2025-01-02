using System.ComponentModel.DataAnnotations;

namespace CampusEventHubApi.DTOs
{
    public class ChangePasswordRequest
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }
    }
}
