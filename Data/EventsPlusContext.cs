using Microsoft.EntityFrameworkCore;
using EventsPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlus.Data
{
    public class EventsPlusContext : DbContext
    {
        public EventsPlusContext(DbContextOptions<EventsPlusContext> options) : base(options)
        { 
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventType> EventType { get; set; }
        public DbSet<EventAttendees> EventAttendees { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<ManagerEvents> ManagerEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().ToTable("Events");
            modelBuilder.Entity<EventType>().ToTable("Event Type");
            modelBuilder.Entity<EventAttendees>().ToTable("Event Attendees");
            modelBuilder.Entity<Attendee>().ToTable("Attendees");
            modelBuilder.Entity<Manager>().ToTable("Managers");
            modelBuilder.Entity<ManagerEvents>().ToTable("Managers Events");
        }
    }
}
