using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        private readonly IRepositoryAsync<Course> _courseRepository;
        private readonly DHLProject2SchoolContext _context;

        public CourseController(ILogger<CourseController> logger, IRepositoryAsync<Course> courseRepository)
        {
            _logger = logger;
            _courseRepository = courseRepository;
        }

        // GET "api/Course"
        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            return Ok(await _courseRepository.ToListAsync());
        }

        // GET "api/Course/id"
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            if (await _courseRepository.FindAsync(id) is Course courseItem)
            {
                return Ok(courseItem);
            }
            return NotFound();
        }

        // POST "api/Course"
        [HttpPost]
        public async Task<IActionResult> CreateCourse(string name, string description,int creditValue, int departmentId, int? code, int? session, int? category, int capacity, int waitlistCapacity)
        {
            try
            {
                var CourseItem = new Course { 
                    Name = name, 
                    Description = description, 
                    CreditValue = creditValue,
                    DepartmentId = departmentId,
                    Code = code,
                    Session = session,
                    Category = category,
                    Capacity = capacity, 
                    WaitlistCapacity = waitlistCapacity
                };

                await _courseRepository.AddAsync(CourseItem);

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create Course!");

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT "api/Course/id"
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, Course courseItem)
        {
            try
            {
                var courseToEdit = await _courseRepository.FindAsync(id);

                courseToEdit.Name = courseItem.Name;
                courseToEdit.Description = courseItem.Description;
                courseToEdit.CreditValue = courseItem.CreditValue;
                courseToEdit.DepartmentId = courseItem.DepartmentId;
                courseToEdit.Code = courseItem.Code;
                courseToEdit.Session = courseItem.Session;
                courseToEdit.Category = courseItem.Category;
                courseToEdit.Capacity = courseItem.Capacity;
                courseToEdit.WaitlistCapacity = courseItem.WaitlistCapacity;

                _courseRepository.Update(courseToEdit);

                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update course.");

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE "api/Course/id"
        [HttpDelete]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (await _courseRepository.FindAsync(id) is Course courseItem)
            {
                _courseRepository.Remove(courseItem);

                await _context.SaveChangesAsync();

                return Ok();
            }
            return NotFound();
        }        

    }
}
