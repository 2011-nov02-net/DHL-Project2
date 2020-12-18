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
    public class ClassController : ControllerBase
    {
        private readonly ILogger<ClassController> _logger;
        private readonly DbSet<Class> _classRepository;
        private readonly DHLProject2SchoolContext _context;

        public ClassController(ILogger<ClassController> logger, DHLProject2SchoolContext context)
        {
            _logger = logger;
            _context = context;
            _classRepository = context.Classes;
        }

        // GET "api/Class"
        [HttpGet]
        public async Task<IActionResult> GetClasses()
        {
            return Ok(await _classRepository.ToListAsync());
        }

        // GET "api/Class/id"
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassById(int id)
        {
            if (await _classRepository.FindAsync(id) is Class classItem)
            {
                return Ok(classItem);
            }
            return NotFound();
        }

        // POST "api/Class"
        [HttpPost]
        public async Task<IActionResult> CreateClass(string name, string description, int capacity, DateTime start, DateTime end, Instructor instructo)
        {
            try
            {
                var classItem = new Class { 
                    CourseName = name, 
                    CourseDescription = description, 
                    CourseCapacity = capacity, 
                    StartTime = start, 
                    EndTime = end,
                };

                await _classRepository.AddAsync(classItem);

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create class!");

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT "api/Class/id"
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(int id, Class classItem)
        {
            try
            {
                var classToEdit = await _classRepository.FindAsync(id);

                classToEdit.CourseName = classItem.CourseName;
                classToEdit.CourseDescription = classItem.CourseDescription;
                classToEdit.CourseCapacity = classItem.CourseCapacity;
                classToEdit.StartTime = classItem.StartTime;
                classToEdit.EndTime = classItem.EndTime;
                classToEdit.Instructor = classItem.Instructor;
                classToEdit.Building = classItem.Building;

                _classRepository.Update(classToEdit);

                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update class.");

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE "api/Class/id"
        [HttpDelete]
        public async Task<IActionResult> DeleteClass(int id)
        {
            if (await _classRepository.FindAsync(id) is Class classItem)
            {
                _classRepository.Remove(classItem);

                return Ok();
            }
            return NotFound();
        }        

    }
}
