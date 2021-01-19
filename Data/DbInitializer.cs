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

            var events = new Event[]
            {
                new Event{EventID=1, Name="Aman's 21st Birthday", Description="Celebrating Aman's 21st Birthday", Location="Centre AT7, 12 Bell Green Rd, Coventry CV6 7GP", Duration="4 Hours", SlotsTotal=40, SlotsRemaining=39, DateTime="22-Aug-2021 14:00"}
            };

            foreach (Event e in events)
            {
                context.Events.Add(e);
            }
            context.SaveChanges();

            var managers = new Manager[]
            {
                new Manager{ManagerID=1, Name="Dionne", Phone="04325405", Email="dionne@mail.com" }
            };

            foreach (Manager m in managers)
            {
                context.Managers.Add(m);
            }
            context.SaveChanges();

            var attendees = new Attendee[]
            {
                new Attendee{AttendeeID=1, Name="Bob", Phone="04329402", Address="12 Hello Road", Email="hello@me.com", EventID=1}
            };

            foreach (Attendee a in attendees)
            {
                context.Attendees.Add(a);
            }
            context.SaveChanges();

            var eventType = new EventType[]
            {
                new EventType{EventTypeID=1, Type="Birthday"}
            };

            foreach (EventType et in eventType)
            {
                context.EventType.Add(et);
            }
            context.SaveChanges();
        }
    }
}
