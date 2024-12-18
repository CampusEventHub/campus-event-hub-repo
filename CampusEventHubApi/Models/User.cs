using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        [MinLength(6)]
        public string PasswordHash { get; set; }

        public bool IsAdmin { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(255)]
        [DefaultValue("https://via.placeholder.com/150")]
        public string ImageUrl { get; set; } = "https://via.placeholder.com/150";
    }
}