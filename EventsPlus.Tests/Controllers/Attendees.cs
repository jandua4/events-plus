using EventsPlus.Controllers;
using EventsPlus.Data;
using EventsPlus.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EventsPlus.Tests.Controllers
{
    public class Attendees
    {
        private async Task<EventsPlusContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<EventsPlusContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new EventsPlusContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Attendees.CountAsync() <= 0)
            {
                databaseContext.Attendees.AddRange(Attendee());
                await databaseContext.SaveChangesAsync();
            }
            if (await databaseContext.Events.CountAsync() <= 0)
            {
                databaseContext.Events.AddRange(Event());
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }

        private List<Attendee> Attendee()
        {
            return new List<Attendee>
            {
                new Attendee{ AttendeeID = 11, Name="Test", Phone="032492809", Email="aman@test.com", EventID=11},
                new Attendee{ AttendeeID = 12, Name="Another Test", Phone="032492809", Email="test@aman.com", EventID=11}
            };
        }
        private List<Event> Event()
        {
            return new List<Event>
            {
                new Event{ EventID = 11, Name="Test", Description="blah", Duration="2 hours", ManagerID=null, Location="blah", StartTime=DateTime.Parse("22-Aug-2021 14:00"), EventTypeID=1},
                new Event{ EventID = 12, Name="AnotherTest", Description="blah", Duration="2 hours", ManagerID=null, Location="blah", StartTime=DateTime.Parse("22-Aug-2021 14:00"), EventTypeID=1}
            };
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfAttendees()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var attendeesController = new AttendeesController(dbContext);
            //Act
            var result = await attendeesController.Index("", "", "", null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Attendee>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Details_ReturnsViewResultWithAttendeeModel()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var attendeesController = new AttendeesController(dbContext);

            //Act
            var result = await attendeesController.Details(11);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Attendee>(
                viewResult.ViewData.Model);
            Assert.Equal(11, model.AttendeeID);
            Assert.Equal("Test", model.Name);
            Assert.Equal("032492809", model.Phone);
            Assert.Equal("aman@test.com", model.Email);
            Assert.Equal(11, model.EventID);
        }

        [Fact]
        public async Task Edit_ReturnsHttpNotFoundWhenAttendeeIdNotFound()
        {
            //Arrange
            int AttendeeID = 4;
            var dbContext = await GetDatabaseContext();
            var attendeesController = new AttendeesController(dbContext);

            //Act
            var result = await attendeesController.Edit(AttendeeID);

            //Assert
            Assert.IsType<NotFoundResult>(result);

        }
    }
}