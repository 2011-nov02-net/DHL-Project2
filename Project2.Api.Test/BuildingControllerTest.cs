using System;
using Xunit;
using Moq;
using Project2.DataModel;
using System.Collections.Generic;
using Project2.Api.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Project2.Api.Test
{
    public class BuildingControllerTest
    {
        [Fact]
        public async void GetBuildingById_ReturnsBuilding_OKObjectResult()
        {
            // Arrange
            int id = 1;
            var mockBuildingRepo = new Mock<IRepositoryAsync<Building>>();
            mockBuildingRepo.Setup(repo => repo.FindAsync(id))
                .ReturnsAsync(new Building { Name = "Test", Id = id});
            var controller = new BuildingController(null, mockBuildingRepo.Object);

            // Act
            var result = await controller.GetBuildingById(id) as ObjectResult;

            // Assert
            var OkResult = Assert.IsType<OkObjectResult>(result);
            var building = Assert.IsAssignableFrom<Building>(result.Value);
            Assert.Equal(1, building.Id);
            Assert.Equal("Test", building.Name);
        }

        [Fact]
        public async void GetBuildingById_BuildingNotExists_NotFoundResult()
        {
            int id = 1;
            var mockBuildingRepo = new Mock<IRepositoryAsync<Building>>();
            //mockBuildingRepo.Setup(repo => repo.FindAsync(id))
            //    .ReturnsAsync(null);
            var controller = new BuildingController(null, mockBuildingRepo.Object);

            // Act
            var result = await controller.GetBuildingById(id);

            // Assert
            var NotFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, NotFoundResult.StatusCode);
        }

        [Fact]
        public async void CreateBuilding_CreatesAndAddsBuilding_OKResult()
        {
            var mockBuildingRepo = new Mock<IRepositoryAsync<Building>>();
            var controller = new BuildingController(null, mockBuildingRepo.Object);

            // Act
            var result = await controller.CreateBuilding("Test");

            // Assert
            var OkResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void CreateBuilding_NullBuildingName_OKResult()
        {
            var mockBuildingRepo = new Mock<IRepositoryAsync<Building>>();
            var controller = new BuildingController(null, mockBuildingRepo.Object);

            // Act
            var result = await controller.CreateBuilding(null);

            // can't test this with errors cause nothing throws errors in the try block yet
            // Assert
            var OkResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void UpdateBuilding_NoContentResult()
        {
            // Arrange
            int id = 1;
            var mockBuildingRepo = new Mock<IRepositoryAsync<Building>>();
            mockBuildingRepo.Setup(repo => repo.FindAsync(id))
                .ReturnsAsync(new Building { Name = "Test", Id = 1, Rooms = null });
            var controller = new BuildingController(null, mockBuildingRepo.Object);
            var building = new Building() { Name = "Test" };

            // Act
            var result = await controller.UpdateBuilding(1, building);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void DeleteBuilding_OkResult()
        {
            // Arrange
            int id = 1;
            var mockBuildingRepo = new Mock<IRepositoryAsync<Building>>();
            mockBuildingRepo.Setup(repo => repo.FindAsync(id))
                .ReturnsAsync(new Building { Name = "Test", Id = 1, Rooms = null });
            var controller = new BuildingController(null, mockBuildingRepo.Object);

            // Act
            var result = await controller.DeleteBuilding(id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void DeleteBuilding_BuildingDoesNotExist_NotFoundResult()
        {
            // Arragne
            int id = 1;
            var mockBuildingRepo = new Mock<IRepositoryAsync<Building>>();
            var controller = new BuildingController(null, mockBuildingRepo.Object);

            // Act
            var result = await controller.DeleteBuilding(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
