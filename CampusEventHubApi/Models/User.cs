using System;
using System.ComponentModel.DataAnnotations;


namespace CampusEventHubApi.Models
{
    public class User
    {
        [Key]
        public int IDUser { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(128)]
        public string Salt { get; set; } 

        public bool IsAdmin { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(255)]
        public string ImageUrl { get; set; } = "https://via.placeholder.com/150";
    }
}
