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
        public EventsPlusContext (DbContextOptions<EventsPlusContext> options) : base(options)
        { 
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Manager> Managers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set Integrity Constraints and Build Tables

            // Events
            modelBuilder.Entity<Event>()
                .ToTable("Events")
                .HasKey(e => e.EventID);

            modelBuilder.Entity<Event>()
                .HasMany(a => a.Attendees)
                .WithOne(e => e.Event)
                .HasForeignKey(a => a.AttendeeID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event>()
                .HasOne(m => m.Manager)
                .WithOne(e => e.Event)
                .HasForeignKey<Event>(m => m.ManagerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event>()
                .HasOne(t => t.EventType)
                .WithMany(e => e.Events)
                .HasForeignKey(t => t.EventTypeID)
                .OnDelete(DeleteBehavior.NoAction);


            // Event Type
            modelBuilder.Entity<EventType>()
                .ToTable("EventTypes")
                .HasKey(t => t.EventTypeID);

            modelBuilder.Entity<EventType>()
                .HasMany(t => t.Events)
                .WithOne(t => t.EventType)
                .OnDelete(DeleteBehavior.SetNull);


            // Attendees
            modelBuilder.Entity<Attendee>()
                .ToTable("Attendees")
                .HasKey(a => a.AttendeeID);

            modelBuilder.Entity<Attendee>()
                .HasOne(e => e.Event)
                .WithMany(a => a.Attendees)
                .HasForeignKey(e => e.EventID)
                .OnDelete(DeleteBehavior.NoAction);


            // Managers
            modelBuilder.Entity<Manager>()
                .ToTable("Managers")
                .HasKey(m => m.ManagerID);

            modelBuilder.Entity<Manager>()
                .HasOne(e => e.Event)
                .WithOne(m => m.Manager)
                .HasForeignKey<Manager>(e => e.EventID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
