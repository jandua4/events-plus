using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlus.Models
{
    public class Event
    {
        public int EventID { get; set; }

        [Required]
        [Display(Name = "Event Name")]
        [StringLength(50, ErrorMessage = "Max 50 Characters")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date/Time")]
        [StringLength(20, ErrorMessage = "Max 20 Characters")]
        public DateTime DateTime { get; set; }

        [Required]
        [Display(Name = "Location")]
        [StringLength(255, ErrorMessage = "Max 255 Characters")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Duration")]
        [StringLength(15, ErrorMessage = "Max 15 Characters")]
        public string Duration { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(255, ErrorMessage = "Max 255 Characters")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Slots Remaining")]
        [StringLength(10, ErrorMessage = "Max 10 Characters")]
        public int SlotsRemaining { get; set; }

        [Required]
        [Display(Name = "Slots Total")]
        [StringLength(10, ErrorMessage = "Max 10 Characters")]
        public int SlotsTotal { get; set; }

        [Display(Name = "Event Type")]
        public int? EventTypeID { get; set; }

        [Display(Name = "Event Manager (Not Required)")]
        public int? ManagerID { get; set; }

        // Navigation
        /* Set up relationship with other entities
        ** Self-instantiated objects have one entity
        ** ICollection can hold multiple entities */
        public EventType EventType { get; set; }
        public Manager Manager { get; set; }
        public ICollection<Attendee> Attendees { get; set; }
    }
}
