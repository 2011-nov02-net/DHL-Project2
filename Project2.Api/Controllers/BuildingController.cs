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

        // GET "api/Building/id/room"
        [HttpGet("{id}/room")]
        public async Task<IActionResult> GetRoomsByBuildingId(int id)
        {
            if (await _buildingRepository.FindAsync(id) is Building building)
            {
                var buildingRooms = building.Rooms.ToList();
                return Ok(buildingRooms);
            }
            return NotFound();
        }

        // GET "api/Building/id/room/roomId"
        [HttpGet("{id}/room/{roomId}")]
        public async Task<IActionResult> GetRoomByRoomId(int id, int roomId)
        {
            try 
            {
                if (await _buildingRepository.FindAsync(id) is Building building && 
                    building.Rooms.FirstOrDefault(r => r.Id == roomId) is Room room)
                {
                    return Ok(room);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT "api/Building/id/room/roomId"
        [HttpPut("{id}/room/{roomId}")]
        public async Task<IActionResult> UpdateBuildingRooms(int id, int roomId, int capacity)
        {
            try
            {
                var building = await _buildingRepository.FindAsync(id);
                if (await building.Rooms.AsQueryable().FirstOrDefaultAsync(r => r.Id == roomId) is Room room)
                {
                    //update the room
                    room.Capacity = capacity;
                    await _buildingRepository.UpdateAsync(building);
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to update building room.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // POST "api/Building/id/room"
        [HttpPost("{id}/room")]
        public async Task<IActionResult> CreateRoomForBuilding(int buildingId, decimal number, int capacity)
        {
            try
            {
                var building = await _buildingRepository.FindAsync(buildingId);
                var room = new Room { Number = number, Capacity = capacity, BuildingId = buildingId };
                building.Rooms.Add(room);
                await _buildingRepository.UpdateAsync(building);
                return Created($"api/Building/{buildingId}/room/{room.Id}", room);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create room.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);                    
            }
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
                buildingToEdit.Rooms = building.Rooms;

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
        // GET "api/Buidling/id/Room"
        [HttpGet("{id}/Room")]
        public async Task<IActionResult> GetBuidlingRooms(int id)
        {
            if (await _buildingRepository.FindAsync(id) is Building buidling)
            {
                return Ok(buidling.Rooms);  
            }
            return NotFound();
        }

        // DELETE "api/Building/id/room/id"
        [HttpDelete("{id}/Room/{roomId}")]
        public async Task<IActionResult> DeleteRoomFromBuilding(int id, int roomId)
        {
            try
            {
                if (await _buildingRepository.FindAsync(id) is Building buidling)
                {
                    if (await buidling.Rooms.AsQueryable()
                        .FirstOrDefaultAsync(r => r.Id == roomId) is Room room)
                    {
                        buidling.Rooms.Remove(room);
                        await _buildingRepository.UpdateAsync(buidling);
                        return Ok(); 
                    }
                    return NotFound();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error");
                return NotFound();
            }
        }
    }
}
