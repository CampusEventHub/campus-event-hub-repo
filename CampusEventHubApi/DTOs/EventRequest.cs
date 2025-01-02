using System;
using System.ComponentModel.DataAnnotations;

namespace CampusEventHubApi.DTOs
{
    public class EventRequest
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        [MaxLength(255)]
        public string Location { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int UserID { get; set; }
    }
}
