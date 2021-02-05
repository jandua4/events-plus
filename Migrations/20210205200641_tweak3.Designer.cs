﻿// <auto-generated />
using System;
using EventsPlus.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EventsPlus.Migrations
{
    [DbContext(typeof(EventsPlusContext))]
    [Migration("20210205200641_tweak3")]
    partial class tweak3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("EventsPlus.Models.Attendee", b =>
                {
                    b.Property<int>("AttendeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("EventID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("AttendeeID");

                    b.HasIndex("EventID");

                    b.ToTable("Attendees");
                });

            modelBuilder.Entity("EventsPlus.Models.Event", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("EventTypeID")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("ManagerID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("EventID");

                    b.HasIndex("EventTypeID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("EventsPlus.Models.EventType", b =>
                {
                    b.Property<int>("EventTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("EventTypeID");

                    b.ToTable("EventTypes");
                });

            modelBuilder.Entity("EventsPlus.Models.Manager", b =>
                {
                    b.Property<int>("ManagerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("EventID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("ManagerID");

                    b.HasIndex("EventID")
                        .IsUnique();

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("EventsPlus.Models.Attendee", b =>
                {
                    b.HasOne("EventsPlus.Models.Event", "Event")
                        .WithMany("Attendees")
                        .HasForeignKey("EventID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("EventsPlus.Models.Event", b =>
                {
                    b.HasOne("EventsPlus.Models.EventType", "EventType")
                        .WithMany("Events")
                        .HasForeignKey("EventTypeID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("EventType");
                });

            modelBuilder.Entity("EventsPlus.Models.Manager", b =>
                {
                    b.HasOne("EventsPlus.Models.Event", "Event")
                        .WithOne("Manager")
                        .HasForeignKey("EventsPlus.Models.Manager", "EventID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("EventsPlus.Models.Event", b =>
                {
                    b.Navigation("Attendees");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("EventsPlus.Models.EventType", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}