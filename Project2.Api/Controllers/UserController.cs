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
        private readonly IRepositoryAsync<User> _userRepository;
        public UserController(ILogger<UserController> logger, IRepositoryAsync<User> userRepository)
        {
            _userRepository = userRepository;
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
            try
            {
                if (await _userRepository.FindAsync(id)
                    is User user) return Ok(user);
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create user.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("find/{email}")]
        public async Task<IActionResult> GetUser(string email)
        {
            if (await _userRepository.FirstOrDefaultAsync(u => u.Email == email)
                is User user) return Ok(user);
            return NotFound();

        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(string name, string email, int permission)
        {
            try
            {
                var user = new User { FullName = name, Email = email, Permission = permission };
                if (await _userRepository.AddAsync(user)) 
                    return CreatedAtAction( 
                        actionName: nameof(GetUser),
                        routeValues: new { user.Id },
                        value: user);
                else return NoContent();
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
            var userEnrollment = await _userRepository.Include(u => u.Enrollments).Where(u => u.Id == id).ToListAsync();
            if (userEnrollment.Count > 0)
            {
                var enrollments = userEnrollment.First().Enrollments.ToList();
                if(enrollments.Count > 0)
                    return Ok(enrollments);
            }
            return NotFound();
      }
        [HttpGet("{id}/transcript")]
        public async Task<IActionResult> GetUserTranscript(int id)
        {
            if (_userRepository.FirstOrDefault(x => x.Id == id) is User user)
            {
                var gradedCourses = user.Enrollments.Where(x => x.Grade != null);
                return Ok(await gradedCourses.AsQueryable().ToListAsync());
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