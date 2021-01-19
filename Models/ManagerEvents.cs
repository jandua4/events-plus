using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlus.Models
{
    public class ManagerEvents
    {
        public int ManagerEventID { get; set; }
        public int ManagerID { get; set; }
        public int EventID { get; set; }

        public Event Events { get; set; }
        public Manager Manager { get; set; }
    }
}
