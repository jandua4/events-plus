using EventsPlus.Models;

namespace EventsPlus.ViewModels
{
    public class AttendeeEventViewModel
    {
        // To be used for compiling list of attendees of an event
        public Attendee Attendee { get; set; }
        public Event Event { get; set; }
    }
}
