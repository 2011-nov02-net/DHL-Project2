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
    public class DepartmentControllerTest
    {
        /*
         * List of routes/methods not tested currently
         * /baseroute - GetDepartments()
         * 
         */

        [Fact]
        public async void GetDepartmentById_OkObjectResult()
        {
            //Arrange
            var id = 1;
            var mockDepartmentRepo = new Mock<IRepositoryAsync<Department>>();
            mockDepartmentRepo.Setup(repo => repo.FindAsync(id))
                .ReturnsAsync(new Department());
            var controller = new DepartmentController(null, mockDepartmentRepo.Object);

            // Act
            var result = await controller.GetDepartmentById(id) as ObjectResult;

            // Assert
            var OkResult = Assert.IsType<OkObjectResult>(result);
            var department = Assert.IsAssignableFrom<Department>(result.Value); 
        }

        [Fact]
        public async void GetDepartmentById_NotFoundResult()
        {
            //Arrange
            var id = 1;
            var mockDepartmentRepo = new Mock<IRepositoryAsync<Department>>();
            var controller = new DepartmentController(null, mockDepartmentRepo.Object);

            // Act
            var result = await controller.GetDepartmentById(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void PostDepartment_OkResult()
        {
            // Arrange
            var department = new Department();
            var mockDepartmentRepo = new Mock<IRepositoryAsync<Department>>();
            var controller = new DepartmentController(null, mockDepartmentRepo.Object);

            // Act
            var result = await controller.PostDepartment(department);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void PutDepartment_OkResult()
        {
            // Arrange
            int id = 1;
            var department = new Department();
            var mockDepartmentRepo = new Mock<IRepositoryAsync<Department>>();
            var controller = new DepartmentController(null, mockDepartmentRepo.Object);

            // Act
            var result = await controller.PutDepartment(id, department);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void DeleteDepartmentById_OkResult()
        {
            //Arrange
            var id = 1;
            var mockDepartmentRepo = new Mock<IRepositoryAsync<Department>>();
            mockDepartmentRepo.Setup(repo => repo.FindAsync(id))
                .ReturnsAsync(new Department());
            var controller = new DepartmentController(null, mockDepartmentRepo.Object);

            // Act
            var result = await controller.DeleteDepartmentById(id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void DeleteDepartmentById_NotFoundResult()
        {
            //Arrange
            var id = 1;
            var mockDepartmentRepo = new Mock<IRepositoryAsync<Department>>();
            var controller = new DepartmentController(null, mockDepartmentRepo.Object);

            // Act
            var result = await controller.DeleteDepartmentById(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
