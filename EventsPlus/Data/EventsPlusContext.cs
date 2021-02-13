using EventsPlus.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventsPlus.Data
{
    public class EventsPlusContext : IdentityDbContext
    {
        public EventsPlusContext(DbContextOptions<EventsPlusContext> options) : base(options)
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
            //Set Attendee relationship and constraint
            modelBuilder.Entity<Event>()
                .HasMany(a => a.Attendees)
                .WithOne(e => e.Event)
                .HasForeignKey(a => a.EventID)
                .OnDelete(DeleteBehavior.Cascade);
            //Set Manager relationship and constraint
            modelBuilder.Entity<Event>()
                .HasOne(m => m.Manager)
                .WithMany(e => e.Events)
                .HasForeignKey(m => m.ManagerID)
                .OnDelete(DeleteBehavior.SetNull);
            //Set Event Type relationship and constraint
            modelBuilder.Entity<Event>()
                .HasOne(t => t.EventType)
                .WithMany(e => e.Events)
                .HasForeignKey(t => t.EventTypeID)
                .OnDelete(DeleteBehavior.SetNull);


            // Event Type
            modelBuilder.Entity<EventType>()
                .ToTable("EventTypes")
                .HasKey(t => t.EventTypeID);


            // Attendees
            modelBuilder.Entity<Attendee>()
                .ToTable("Attendees")
                .HasKey(a => a.AttendeeID);


            // Managers
            modelBuilder.Entity<Manager>()
                .ToTable("Managers")
                .HasKey(m => m.ManagerID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
