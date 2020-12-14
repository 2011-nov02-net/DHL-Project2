using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Project2.DataModel;

namespace Project2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly DbSet<Person> _personRepository;
        public PersonController(DbSet<Person> personRepository)
        {
            _personRepository = personRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {            
            return Ok(await _personRepository.ToListAsync() );
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            if (await _personRepository.FirstOrDefaultAsync(u => u.Id == id) 
                    is Person person) return Ok(person);
            return NotFound();
        }
    }
}