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
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly DbSet<User> _userRepository;
        private readonly DHLProject2SchoolContext _context;
        public UserController(ILogger<UserController> logger, DHLProject2SchoolContext context)
        {
            _context = context;
            _userRepository = context.Users;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userRepository.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            if (await _userRepository.FindAsync(id)
                    is User user) return Ok(user);
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(string name, string email, int permission)
        {
            try
            {
                var user = new User { FullName = name, Email = email, Permission = permission };
                await _userRepository.AddAsync(user);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create user.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            try
            {
                var oldUser = await _userRepository.FindAsync(id);
                oldUser.FullName = user.FullName;
                oldUser.Email = user.Email;
                oldUser.Permission = user.Permission;
                _userRepository.Update(oldUser);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update User.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (await _userRepository.FindAsync(id) is User user)
            {
                _userRepository.Remove(user);
                return Ok();
            }
            return NotFound();
        }
        [HttpGet("{id}/courses")]
        public async Task<IActionResult> GetUserCourses(int id)
        {
            if (await _userRepository
                .Include(p => p.Enrollments).ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(user => user.Id == id)
                is User user)
            {
                var courses = user.Enrollments.Select(x => x.Course).AsQueryable().ToListAsync();
                return Ok(await courses);
            }
            return NotFound();
        }
        [HttpGet("{id}/transcript")]
        public async Task<IActionResult> GetUserTranscript(int id)
        {
            var gradedCourses = await _context.Enrollments.Where(e => e.Grade != null && e.User == id).ToListAsync();
            if (gradedCourses != null)
            {
                return Ok(gradedCourses);
            }
            return NotFound();
        }
        [HttpPost("{id}/courses/{courseId}")]
        public async Task<IActionResult> UpdateUserCourse(int id, int courseId)
        {
            try
            {
                if (await _userRepository
                    .Include(p => p.Enrollments).ThenInclude(e => e.Course)
                    .FirstOrDefaultAsync(user => user.Id == id)
                    is User user)
                {
                    user.Enrollments.Add(new Enrollment { Course = courseId });
                    _userRepository.Update(user);
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update user enrollments.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}