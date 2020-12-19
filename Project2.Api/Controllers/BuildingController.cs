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

        // GET "api/Building/id/room"
        [HttpGet("{id}/room")]
        public async Task<IActionResult> GetRoomsByBuildingId(int id)
        {
            var buildingRooms = await _context.Rooms.Where(r => r.BuildingId == id).ToListAsync();
            if (buildingRooms != null)
            {
                return Ok(buildingRooms);
            }
            return NotFound();
        }

        // GET "api/building/id/room/roomId"
        [HttpGet("{id}/room/{roomId}")]
        public async Task<IActionResult> GetRoomByRoomId(int roomId)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
            if (room != null)
            {
                return Ok(room);
            }
            return NotFound();

        }

        // PUT "api/Building/id/room/roomId"
        [HttpPut("{id}/room/{roomId}")]
        public async Task<IActionResult> UpdateBuildingRooms(int roomId, int capacity)
        {
            try
            {
                var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
                if (room != null)
                {
                    //update the room
                    room.Capacity = capacity;
                    _context.Update(room);
                    await _context.SaveChangesAsync();
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
                var room = new Room { Number = number, Capacity = capacity, BuildingId = buildingId };
                var building = await _buildingRepository.Where(b => b.Id == buildingId).FirstOrDefaultAsync();
                building.Rooms.Add(room);
                await _context.SaveChangesAsync();
                return Ok();
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

                await _context.SaveChangesAsync();

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

                _buildingRepository.Update(buildingToEdit);

                await _context.SaveChangesAsync();

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

                await _context.SaveChangesAsync();

                return Ok();
            }
            return NotFound();
        }

        // DELETE "api/Building/id/room/id"
        [HttpDelete("{id}/Room/{roomId}")]
        public async Task<IActionResult> DeleteRoomFromBuilding(int id, int roomId)
        {
            var buildingRoom = await _context.Rooms.Where(r => r.BuildingId == id).FirstOrDefaultAsync(r => r.Id == roomId);
            var building = await _buildingRepository.Where(b => b.Id == id).FirstOrDefaultAsync();

            if (building != null && buildingRoom != null)
            {
                building.Rooms.Remove(buildingRoom);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
            
        }


    }
}
