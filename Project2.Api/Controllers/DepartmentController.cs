using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2.DataModel;

namespace Project2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly DbSet<Department> _departmentRepository;
        public DepartmentController(ILogger<DepartmentController> logger, DHLProject2SchoolContext context)
        {
            _departmentRepository = context.Departments;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetDepartments()
        {
            return Ok( await _departmentRepository.ToListAsync() );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartmentById(int id)
        {
            if (await _departmentRepository.FindAsync(id) is Department department)
            {
                return Ok(department);
            }
            return NotFound();
        }

        [HttpPost("")]
        public async Task<ActionResult<Department>> PostDepartment(Department model)
        {
            try
            {
                await _departmentRepository.AddAsync(model);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); 
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department model)
        {
            try
            {
                model.Id = id;
                await _departmentRepository.AddAsync(model);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> DeleteDepartmentById(int id)
        {
            if (await _departmentRepository.FindAsync(id) is Department department)
            {
                _departmentRepository.Remove(department);
                return Ok();
            }
            return NotFound();
        }
    }
}