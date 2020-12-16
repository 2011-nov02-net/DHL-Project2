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
    public class BuildingController : ControllerBase
    {
        private readonly ILogger<BuildingController> _logger;
        private readonly DbSet<Building> _buildingRepository;
        private readonly DHLProject2SchoolContext _context;

        public BuildingController(ILogger<BuildingController> logger, DHLProject2SchoolContext context)
        {
            _logger = logger;
            _context = context;
            _buildingRepository = context.Buildings;
        }

        // GET "api/Building"
        [HttpGet]
        public async Task<IActionResult> GetBuildings()
        {
            return Ok(await _buildingRepository.ToListAsync());
        }

        // GET "api/Building/id"
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBuildingById(int id)
        {
            if (await _buildingRepository.FindAsync(id) is Building buildingItem)
            {
                return Ok(buildingItem);
            }
            return NotFound();
        }

        // POST "api/Building"
        [HttpPost]
        public async Task<IActionResult> CreateBuilding(string name)
        {
            try
            {
                var building = new Building { BuildingName = name };

                await _buildingRepository.AddAsync(building);

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create building.");

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        // PUT "api/Building/id"
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBuilding(int id, Building building)
        {
            try
            {
                var buildingToEdit = await _buildingRepository.FindAsync(id);

                buildingToEdit.BuildingName = building.BuildingName;

                _buildingRepository.Update(buildingToEdit);

                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update building.");

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            
        }

        // DELETE "api/Building/id"
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilding(int id)
        {
            if (await _buildingRepository.FindAsync(id) is Building building)
            {
                _buildingRepository.Remove(building);

                return Ok();
            }
            return NotFound();
        }


    }
}
