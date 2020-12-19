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
        private readonly IRepositoryAsync<Building> _buildingRepository;
        public BuildingController(ILogger<BuildingController> logger, IRepositoryAsync<Building> buildingRepository)
        {
            _logger = logger;
            _buildingRepository = buildingRepository;
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
                var building = new Building { Name = name };

                await _buildingRepository.AddAsync(building);

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

                buildingToEdit.Name = building.Name;

                await _buildingRepository.UpdateAsync(buildingToEdit);

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
                await _buildingRepository.RemoveAsync(building);

                return Ok();
            }
            return NotFound();
        }


    }
}
