using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlus.Models
{
    public class EventAttendees
    {
        public string EventAttendeeID { get; set; }
        public int EventID { get; set; }
        public int AttendeeID { get; set; }

        public Attendee Attendees { get; set; }
        public Event Events { get; set; }
    }
}
