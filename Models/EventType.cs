using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlus.Models
{
    public class EventType
    {
        public int EventTypeID { get; set; }

        [Required]
        [Display(Name = "Event Type")]
        [StringLength(20, ErrorMessage = "Max 20 Characters")]
        public string Type { get; set; }

        // Navigation
        // Indicates a One-To-Many Relationship
        public ICollection<Event> Events { get; set; }
    }
}
