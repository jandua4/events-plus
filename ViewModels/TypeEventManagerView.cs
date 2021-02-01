using EventsPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlus.ViewModels
{
    public class TypeEventManagerViewModel
    {
        // Complete List for Returning to user
        public Event Event { get; set; }
        public EventType EventType { get; set; }
        public Manager Manager { get; set; }
    }
}
