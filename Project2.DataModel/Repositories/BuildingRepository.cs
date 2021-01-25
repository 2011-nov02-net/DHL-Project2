using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project2.Domain.Interfaces;

namespace Project2.DataModel.Repositories
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly DHLProject2SchoolContext _context;
        
        public BuildingRepository(DHLProject2SchoolContext context)
        {
            _context = context;
        }

        public async Task AddBuilding(string buildingName)
        {
            // make new building
            Building building = new Building{ Name = buildingName, Rooms = new List<Room>()};
            // add to context
            await _context.Buildings.AddAsync(building);
            // save results
            await _context.SaveChangesAsync();
        }

        public ValueTask AddRoomToBuilding(int buildingId, decimal roomNum, int capacity)
        {
            throw new NotImplementedException();
        }

        public ValueTask DeleteBuilding(int id)
        {
            throw new NotImplementedException();
        }

        public ValueTask DeleteBuildingRoom(int buildingId, int roomId)
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Models.Building> GetBuilding(int id)
        {
            var building = await _context.Buildings.FirstOrDefaultAsync(x => x.Id == id);
            return new Domain.Models.Building(building.Id, building.Name) ?? null;
        }

        public async Task<ICollection<Domain.Models.Building>> GetBuildings()
        {
            var buildings = await _context.Buildings.ToListAsync();
            //var rooms = buildings.Select(x => x.Rooms.Select(y => new Domain.Models.Room(y.Id, y.Number, y.Capacity ?? 0, y.BuildingId)));
            return buildings.Select(x => new Domain.Models.Building(x.Id, x.Name)).ToList();
        }

        public async Task<ICollection<Domain.Models.Room>> GetRoomsByBuilding(int id)
        {
            var building = await _context.Buildings.FirstOrDefaultAsync(x => x.Id == id);
            return building.Rooms.Select(x => new Domain.Models.Room(x.Id, x.Number, x.Capacity ?? 0, x.BuildingId)).ToList();
        }

        public Task<Domain.Models.Room> GetRoomWithIdAndBuilding(int buildingId, int roomId)
        {
            throw new NotImplementedException();
        }

        public ValueTask UpdateBuilding(int id, Domain.Models.Building building)
        {
            throw new NotImplementedException();
        }

        public ValueTask UpdateBuildingRoom(int buildingId, int roomId, int newCapacity)
        {
            throw new NotImplementedException();
        }
    }
}
