using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlus.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string Name { get; set; }
        public string DateTime { get; set; }
        public string Location { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public int SlotsRemaining { get; set; }
        public int SlotsTotal { get; set; }

        /* Set up relationship with other entities
        ** Self-instantiated objects have one entity
        ** ICollection can hold multiple entities */
        public EventType EventType { get; set; }
        public Manager Manager { get; set; }
        public ICollection<Attendee> Attendees { get; set; }
    }
}
