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
    public class EventTypes
    {
        private async Task<EventsPlusContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<EventsPlusContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new EventsPlusContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.EventTypes.CountAsync() <= 0)
            {
                databaseContext.EventTypes.AddRange(EventType());
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }

        private List<EventType> EventType()
        {
            return new List<EventType>
            {
                new EventType{ EventTypeID = 11, Type="Sport"},
                new EventType{ EventTypeID = 12, Type="Meeting"}
            };
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfEventTypes()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var eventTypesController = new EventTypesController(dbContext);
            //Act
            var result = await eventTypesController.Index("", "", "", null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<EventType>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Details_ReturnsViewResultWithEventTypeModel()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var eventTypesController = new EventTypesController(dbContext);

            //Act
            var result = await eventTypesController.Details(11);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<EventType>(
                viewResult.ViewData.Model);
            Assert.Equal(11, model.EventTypeID);
            Assert.Equal("Sport", model.Type);
        }

        [Fact]
        public async Task Edit_ReturnsHttpNotFoundWhenEventTypeIdNotFound()
        {
            //Arrange
            int EventTypeID = 4;
            var dbContext = await GetDatabaseContext();
            var eventTypesController = new EventTypesController(dbContext);
            
            //Act
            var result = await eventTypesController.Edit(EventTypeID);

            //Assert
            Assert.IsType<NotFoundResult>(result);

        }
    }
}