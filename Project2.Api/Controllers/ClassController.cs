using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Project2.DataModel;

namespace Project2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly ILogger<ClassController> _logger;
        private readonly DHLProject2SchoolContext _context;

        public ClassController(ILogger<ClassController> logger, DHLProject2SchoolContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetClass()
        {
            var classes = await _context.Classes.ToListAsync();
            return Ok(classes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassById(int id)
        {
            if (await _context.Classes.FindAsync(id) is Class course)
            {
                return Ok(course);
            }
            return NoContent();
        }
    }
}