using System;
using System.Threading.Tasks;
// using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
// using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Project2.DataModel;

namespace Project2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly DbSet<Person> _personRepository;
        private readonly DHLProject2SchoolContext _context;
        public PersonController(ILogger<PersonController> logger, DHLProject2SchoolContext context)
        {
            _context = context;
            _personRepository = context.People;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {            
            return Ok(await _personRepository.ToListAsync() );
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(int id)
        {
            if (await _personRepository.FindAsync(id) 
                    is Person person) return Ok(person);
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePerson(string name, string email, string role)
        {
            try 
            {
                var person = new Person {Name = name, Email = email, Role = role }; 
                await _personRepository.AddAsync(person);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create person.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, Person person)
        {
            try
            {
                var oldPerson = await _personRepository.FindAsync(id);
                oldPerson.Name = person.Name;
                oldPerson.Email = person.Email;
                oldPerson.Role = person.Role;
                _personRepository.Update(oldPerson);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create person.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            if (await _personRepository.FindAsync(id) is Person person)
            {
                _personRepository.Remove(person);
                return Ok();
            }
            return NotFound();
        }
        [HttpGet("{id}/courses")]
        public async Task<IActionResult> GetPersonCourses(int id)
        {
            if (await _personRepository
                .Include(p => p.Enrollments).ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(person => person.Id == id) 
                is Person person)
            {
                var courses = person.Enrollments.Select(x => x.Course).AsQueryable().ToListAsync();
                return Ok(await courses);
            }
            return NotFound();
        }
        [HttpGet("{id}/transcript")]
        public async Task<IActionResult> GetPersonTranscript(int id)
        {
            if (await _personRepository
                .Include(p => p.Transcripts).ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(person => person.Id == id) 
                is Person person)
            {
                var courses = person.Enrollments.Select(x => x.Course).AsQueryable().ToListAsync();
                return Ok(await courses);
            }
            return NotFound();
        }
        [HttpPost("{id}/courses/{courseId}")]
        public async Task<IActionResult> UpdatePersonCourse(int id, int courseId)
        {
            try
            {
                if (await _personRepository
                    .Include(p => p.Enrollments).ThenInclude(e => e.Course)
                    .FirstOrDefaultAsync(person => person.Id == id) 
                    is Person person)
                {
                    person.Enrollments.Add(new Enrollment { CourseId = courseId});
                    _personRepository.Update(person);
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create person.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
            }
        }
    }
}