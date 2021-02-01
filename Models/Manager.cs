using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlus.Models
{
    public class Manager
    {
        public int ManagerID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int EventID { get; set; }

        // One manager - potentially many events
        public ICollection<Event> Event { get; set; }
    }
}
