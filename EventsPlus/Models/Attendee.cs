﻿using System.ComponentModel.DataAnnotations;

namespace EventsPlus.Models
{
    public class Attendee
    {
        public int AttendeeID { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "Max 50 Characters")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Phone No.")]
        [StringLength(15, ErrorMessage = "Max 15 Characters")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [StringLength(60, ErrorMessage = "Max 60 Characters")]
        public string Email { get; set; }

        [Display(Name = "Event")]
        public int EventID { get; set; }

        // Navigation
        // Each Attendee can have one value for Events
        public Event Event { get; set; }
    }
}
