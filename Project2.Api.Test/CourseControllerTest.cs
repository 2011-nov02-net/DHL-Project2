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
    public class CourseControllerTest
    {
        [Fact]
        public async void GetCourseById_OkObjectResult()
        {
            // Arrange
            int id = 1;
            var mockCourseRepo = new Mock<IRepositoryAsync<Course>>();
            mockCourseRepo.Setup(repo => repo.FindAsync(id)).ReturnsAsync(new Course());
            var controller = new CourseController(null, mockCourseRepo.Object);

            // Act
            var result = await controller.GetCourseById(id) as ObjectResult;

            // Assert
            var resultObject = Assert.IsType<OkObjectResult>(result);
            var course = Assert.IsAssignableFrom<Course>(result.Value);
        }

        [Fact]
        public async void GetCourseById_NoCourseFound_NotFoundResult()
        {
            // Arrange
            var mockCourseRepo = new Mock<IRepositoryAsync<Course>>();
            var controller = new CourseController(null, mockCourseRepo.Object);

            // Act
            var result = await controller.GetCourseById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void CreateCourse_OkResult()
        {
            // Arrange
            var mockCourseRepo = new Mock<IRepositoryAsync<Course>>();
            var controller = new CourseController(null, mockCourseRepo.Object);

            // Act
            var result = await controller.CreateCourse("Test", "Test", 1, 1, 1, 1, 1, 1, 1);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void UpdateCourse_NoContentResult()
        {
            // Arrange
            int id = 1;
            var mockCourseRepo = new Mock<IRepositoryAsync<Course>>();
            mockCourseRepo.Setup(repo => repo.FindAsync(id)).ReturnsAsync(new Course());
            var controller = new CourseController(null, mockCourseRepo.Object);
            var courseUpdate = new Course()
            {
                Name = "Test",
                Description = "Test",
                CreditValue = 1,
                DepartmentId = 1,
                Code = 1,
                Session = 1,
                Category = 1,
                Capacity = 1,
                WaitlistCapacity = 1
            };

            // Act
            var result = await controller.UpdateCourse(id, courseUpdate);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void DeleteCourse_OkResult()
        {
            // Arrange
            int id = 1;
            var mockCourseRepo = new Mock<IRepositoryAsync<Course>>();
            mockCourseRepo.Setup(repo => repo.FindAsync(id)).ReturnsAsync(new Course());
            var controller = new CourseController(null, mockCourseRepo.Object);

            // Act
            var result = await controller.DeleteCourse(id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void DeleteCourse_NotFoundResult()
        {
            // Arrange
            int id = 1;
            var mockCourseRepo = new Mock<IRepositoryAsync<Course>>();
            var controller = new CourseController(null, mockCourseRepo.Object);

            // Act
            var result = await controller.DeleteCourse(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void EnrollUserInCourseById_OkResult()
        {
            // Arrange
            int courseId = 1;
            int studentId = 1;
            var mockCourseRepo = new Mock<IRepositoryAsync<Course>>();
            mockCourseRepo.Setup(repo => repo.FindAsync(courseId)).ReturnsAsync(new Course());
            var controller = new CourseController(null, mockCourseRepo.Object);

            // Act
            var result = await controller.EnrollUserInCourseById(courseId, studentId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        // Routes/Methods not tested:
        // /baseroute - GetCourses()
        // /instructor/instructorId - GetCourseEnrollmentById()
        // /id/enrollment/studentId - UpdateEnrollment()
        // cannot test due to incompatible methods to get repos
    }
}
