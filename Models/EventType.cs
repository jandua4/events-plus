using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlus.Models
{
    public class EventType
    {
        public int EventTypeID { get; set; }
        public string Type { get; set; }

        // Indicates a One-To-Many Relationship
        // Can hold many entities
        public ICollection<Event> Events { get; set; }
    }
}
