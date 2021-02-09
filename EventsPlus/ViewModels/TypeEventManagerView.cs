using EventsPlus.Models;

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
