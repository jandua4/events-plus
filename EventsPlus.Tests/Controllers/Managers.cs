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
    public class Managers
    {
        private async Task<EventsPlusContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<EventsPlusContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new EventsPlusContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Managers.CountAsync() <= 0)
            {
                databaseContext.Managers.AddRange(Manager());
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
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
        public async Task Index_ReturnsAViewResult_WithAListOfManagers()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var managersController = new ManagersController(dbContext);
            //Act
            var result = await managersController.Index("", "", "", null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Manager>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Details_ReturnsViewResultWithManagerModel()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var managersController = new ManagersController(dbContext);

            //Act
            var result = await managersController.Details(11);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Manager>(
                viewResult.ViewData.Model);
            Assert.Equal(11, model.ManagerID);
            Assert.Equal("Test", model.Name);
            Assert.Equal("032492809", model.Phone);
            Assert.Equal("aman@test.com", model.Email);
        }

        [Fact]
        public async Task Edit_ReturnsHttpNotFoundWhenManagerIdNotFound()
        {
            //Arrange
            int AttendeeID = 4;
            var dbContext = await GetDatabaseContext();
            var managersController = new ManagersController(dbContext);

            //Act
            var result = await managersController.Edit(AttendeeID);

            //Assert
            Assert.IsType<NotFoundResult>(result);

        }
    }
}