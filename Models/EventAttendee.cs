using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlus.Models
{
    public class EventAttendee
    {
        [ForeignKey("Event")]
        public int EventID { get; set; }

        [ForeignKey("Attendee")]
        public int AttendeeID { get; set; }
    }
}
