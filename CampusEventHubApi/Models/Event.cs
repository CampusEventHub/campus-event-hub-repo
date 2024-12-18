using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusEventHubApi.Models
{
    public class Event
    {
        [Key]
        public int IDEvent { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        [MaxLength(255)]
        public string Location { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public int UserID { get; set; }

        public bool Approved { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(255)]
        [DefaultValue("https://via.placeholder.com/150")]
        public string ImageUrl { get; set; } = "https://via.placeholder.com/150";
    }
}