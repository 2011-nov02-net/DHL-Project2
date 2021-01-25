using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project2.Domain.Interfaces;

namespace Project2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly ILogger<BuildingController> _logger;
        private readonly IBuildingRepository _buildingRepository;
        public BuildingController(ILogger<BuildingController> logger, IBuildingRepository buildingRepository)
        {
            _logger = logger;
            _buildingRepository = buildingRepository;
        }

        // GET "api/Building"
        [HttpGet]
        public async Task<IActionResult> GetBuildings()
        {
            return Ok(await _buildingRepository.GetBuildings());
        }

        // GET "api/Building/id"
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBuildingById(int id)
        {
            if (await _buildingRepository.GetBuilding(id) is Domain.Models.Building buildingItem)
            {
                return Ok(buildingItem);
            }
            return NotFound();
        }

        // GET "api/Building/id/room"
        [HttpGet("{id}/room")]
        public async Task<IActionResult> GetRoomsByBuildingId(int id)
        {
            if (await _buildingRepository.GetRoomsByBuilding(id) is List<Domain.Models.Room> rooms)
            {
                return Ok(rooms);
            }
            return NotFound();
        }

        // GET "api/Building/id/room/roomId"
        [HttpGet("{id}/room/{roomId}")]
        public async Task<IActionResult> GetRoomByRoomId(int id, int roomId)
        {
            try 
            {
                if (await _buildingRepository.GetRoomWithIdAndBuilding(id, roomId) is Domain.Models.Room room)
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
                var room = await _buildingRepository.GetRoomWithIdAndBuilding(id, roomId);
                if (room != null)
                {
                    //update the room
                    room.Capacity = capacity;
                    await _buildingRepository.UpdateBuildingRoom(id, roomId, capacity);
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
                await _buildingRepository.AddRoomToBuilding(buildingId, number, capacity);
                var rooms = await _buildingRepository.GetRoomsByBuilding(buildingId);
                var room = rooms.FirstOrDefault(x => x.Number == number);
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
        public async Task<IActionResult> CreateBuilding([FromForm]string name)
        {
            try
            {
                var building = new Building { Name = name };

                await _buildingRepository.AddBuilding(name);

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
                var buildingToEdit = await _buildingRepository.GetBuilding(id);

                buildingToEdit.Name = building.Name;
                //buildingToEdit.Rooms = building.Rooms;

                await _buildingRepository.UpdateBuilding(id, buildingToEdit);

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
            if (await _buildingRepository.GetBuilding(id) is Domain.Models.Building building)
            {
                await _buildingRepository.DeleteBuilding(id);

                return Ok();
            }
            return NotFound();
        }

        // DELETE "api/Building/id/room/id"
        [HttpDelete("{id}/Room/{roomId}")]
        public async Task<IActionResult> DeleteRoomFromBuilding(int id, int roomId)
        {
            try
            {
                await _buildingRepository.DeleteBuildingRoom(id, roomId);
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
