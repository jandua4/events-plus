using EventsPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlus.Data
{
    public class DbInitializer
    {
        public static void Initialize(EventsPlusContext context)
        {
            context.Database.EnsureCreated();

            if (context.Events.Any())
            {
                return;
            }


            // Events
            var events = new Event[]
            {
                new Event{EventID=1, Name="Aman's 21st Birthday", DateTime="22-Aug-2021 14:00", Location="Centre AT7, 12 Bell Green Rd, Coventry CV6 7GP", Duration="4 Hours", Description="Celebrating Aman's 21st Birthday", SlotsRemaining=39, SlotsTotal=40, EventTypeID=1, }
            };

            foreach (Event e in events)
            {
                context.Events.Add(e);
            }
            context.SaveChanges();


            // Event Types
            var eventType = new EventType[]
            {
                new EventType{ EventTypeID=1, Type="Birthday" }
            };

            foreach (EventType et in eventType)
            {
                context.EventType.Add(et);
            }
            context.SaveChanges();


            // Managers
            var managers = new Manager[]
            {
                new Manager{ManagerID=1, Name="Dionne", Phone="04325405", Email="dionne@mail.com" }
            };

            foreach (Manager m in managers)
            {
                context.Managers.Add(m);
            }
            context.SaveChanges();


            // Attendees
            var attendees = new Attendee[]
            {
                new Attendee{AttendeeID=1, Name="Bob", Phone="04329402", Address="12 Hello Road", Email="hello@me.com" }
            };

            foreach (Attendee a in attendees)
            {
                context.Attendees.Add(a);
            }
            context.SaveChanges();


            // Event Attendee
            var eventAttendee = new EventAttendee[]
            {
                new EventAttendee{ EventID=1, AttendeeID=1 }
            };

            foreach (EventAttendee ea in eventAttendee)
            {
                context.EventAttendees.Add(ea);
            }


            // Manager Event
            var managerEvent = new ManagerEvent[]
            {
                new ManagerEvent{ ManagerID=1, EventID=1 }
            };

            foreach (ManagerEvent me in managerEvent)
            {
                context.ManagerEvents.Add(me);
            }
        }
    }
}
