using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlus.Models
{
    public class ManagerEvent
    {
        [ForeignKey("Manager")]
        public int ManagerID { get; set; }

        [ForeignKey("Event")]
        public int EventID { get; set; }
    }
}
