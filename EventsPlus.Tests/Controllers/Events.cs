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
    public class Events
    {
        private async Task<EventsPlusContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<EventsPlusContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new EventsPlusContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Events.CountAsync() <= 0)
            {
                databaseContext.Events.AddRange(Event());
                await databaseContext.SaveChangesAsync();
            }
            if (await databaseContext.EventTypes.CountAsync() <= 0)
            {
                databaseContext.EventTypes.AddRange(EventType());
                await databaseContext.SaveChangesAsync();
            }
            if (await databaseContext.Managers.CountAsync() <= 0)
            {
                databaseContext.Managers.AddRange(Manager());
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }

        private List<Event> Event()
        {
            return new List<Event>
            {
                new Event{ EventID = 11, Name="Test", Description="blah", Duration="2 hours", ManagerID=11, Location="blah", StartTime=DateTime.Parse("22-Aug-2021 14:00"), EventTypeID=1},
                new Event{ EventID = 12, Name="AnotherTest", Description="blah", Duration="2 hours", ManagerID=11, Location="blah", StartTime=DateTime.Parse("22-Aug-2021 14:00"), EventTypeID=1}
            };
        }

        private List<EventType> EventType()
        {
            return new List<EventType>
            {
                new EventType{ EventTypeID = 1, Type="Sport"},
                new EventType{ EventTypeID = 2, Type="Meeting"}
            };
        }

        private List<Manager> Manager()
        {
            return new List<Manager>
            {
                new Manager{ ManagerID = 11, Name="Test", Phone="032492809", Email="aman@test.com"},
                new Manager{ ManagerID = 12, Name="Another Test", Phone="032492809", Email="test@aman.com"}
            };
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfAttendees()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var eventsController = new EventsController(dbContext);
            //Act
            var result = await eventsController.Index("", "", "", null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Event>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Details_ReturnsViewResultWithAttendeeModel()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var eventsController = new EventsController(dbContext);

            //Act
            var result = await eventsController.Details(11);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Event>(
                viewResult.ViewData.Model);
            Assert.Equal(11, model.EventID);
            Assert.Equal(11, model.ManagerID);
            Assert.Equal(1, model.EventTypeID);
            Assert.Equal("Test", model.Name);
            Assert.Equal("blah", model.Description);
            Assert.Equal("blah", model.Location);
            Assert.Equal("2 hours", model.Duration);
            Assert.Equal(DateTime.Parse("22-Aug-2021 14:00"), model.StartTime);
        }

        [Fact]
        public async Task Edit_ReturnsHttpNotFoundWhenAttendeeIdNotFound()
        {
            //Arrange
            int AttendeeID = 4;
            var dbContext = await GetDatabaseContext();
            var eventsController = new EventsController(dbContext);

            //Act
            var result = await eventsController.Edit(AttendeeID);

            //Assert
            Assert.IsType<NotFoundResult>(result);

        }
    }
}