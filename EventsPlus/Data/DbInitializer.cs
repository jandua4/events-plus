using EventsPlus.Models;
using System;
using System.Linq;

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
                new Event{EventID=1, Name="Aman's 21st Birthday", StartTime=DateTime.Parse("22-Aug-2021 14:00"), Location="Centre AT7, 12 Bell Green Rd, Coventry CV6 7GP", Duration="4 Hours", Description="Celebrating Aman's 21st Birthday", EventTypeID=1, ManagerID=1 }
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
                context.EventTypes.Add(et);
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
                new Attendee{AttendeeID=1, Name="Bob", Phone="04329402", Email="hello@me.com", EventID=1 }
            };

            foreach (Attendee a in attendees)
            {
                context.Attendees.Add(a);
            }
            context.SaveChanges();
        }
    }
}
