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

                await _courseRepository.UpdateAsync(courseToEdit);

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update course.");

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE "api/Course/id"
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (await _courseRepository.FindAsync(id) is Course courseItem)
            {
                await _courseRepository.RemoveAsync(courseItem);

                return Ok();
            }
            return NotFound();
        } 
        
        // POST "api/Course/id/instructor"
        [HttpPost("{courseId}/instructor")]
        public async Task<IActionResult> AddInstructorForCourse(int courseId, int id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
                var instructor = new Instructor { InstructorId = user.Id, CourseId = course.Id };
                if (course != null && user != null)
                {
                    course.Instructors.Add(instructor);
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to add instructor to course");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // GET "api/Course/id/instructor"
        [HttpGet("{id}/instructor")]
        public async Task<IActionResult> GetCourseInstructor(int courseId, int instructorId)
        {
            var courseInstructor = await _context.Instructors.Where(c => c.CourseId == courseId).FirstOrDefaultAsync();

            if (courseInstructor != null)
            {
                return Ok(courseInstructor);
            }
            return NotFound();
        }

        [HttpGet("instructor/{instructorId}")]
        public async Task<IActionResult> GetCoursesWithInstructorById(int instructorId)
        {
            var courses = await _courseRepository.Where(course => course.Instructors
                .Select(x => x.InstructorId).Contains(instructorId)).ToListAsync();
            return Ok(courses);
        }
        [HttpGet("{id}/enrollment")]
        public async Task<IActionResult> GetCourseEnrollmentById(int id)
        {
            var course = await _courseRepository.FindAsync(id);
            var students = course.Enrollments.AsQueryable()
                .Include(x => x.UserNavigation)
            .Select(x => x.UserNavigation);
            return Ok(await students.AsQueryable().ToListAsync());
        }
        [HttpPost("{id}/enrollment/{studentId}")]
        public async Task<IActionResult> EnrollUserInCourseById(int id, int studentId)
        {
            var course = await _courseRepository.FindAsync(id);
            course.Enrollments.Add(new Enrollment { Course = id, User = studentId });
            await _courseRepository.UpdateAsync(course);
            return Ok();
        }
        [HttpPut("{id}/enrollment/{studentId}")]
        public async Task<IActionResult> UpdateEnrollment(int id, int studentId, int gradeId)
        {
            var course = await _courseRepository.FindAsync(id);
            var student = await course.Enrollments.AsQueryable().FirstOrDefaultAsync(x => x.User == studentId);
            student.Grade = gradeId;
            await _courseRepository.UpdateAsync(course);
            return Ok();
        }
    }
}
