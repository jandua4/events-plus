using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlus.Models
{
    public class Events
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string Location { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public int SlotsRemaining { get; set; }
        public int SlotsTotal { get; set; }

        public ICollection<EventAttendees> eventAttendees { get; set; }
        public ICollection<ManagerEvents> managerEvents { get; set; }
    }
}
