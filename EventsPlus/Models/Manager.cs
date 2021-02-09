using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventsPlus.Models
{
    public class Manager
    {
        public int ManagerID { get; set; }

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

        // Navigation
        // Manager can be assigned to events
        public ICollection<Event> Events { get; set; }
    }
}
