using Project2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Domain.Interfaces
{
    public interface IBuildingRepository
    {
        Task<ICollection<Building>> GetBuildings();
        Task<Building> GetBuilding(int id);
        Task<ICollection<Room>> GetRoomsByBuilding(int id);
        Task<Room> GetRoomWithIdAndBuilding(int buildingId, int roomId);
        ValueTask UpdateBuildingRoom(int buildingId, int roomId, int newCapacity);
        ValueTask AddRoomToBuilding(int buildingId, decimal roomNum, int capacity);
        Task AddBuilding(string buildingName);
        ValueTask UpdateBuilding(int id, Building building);
        ValueTask DeleteBuilding(int id);
        ValueTask DeleteBuildingRoom(int buildingId, int roomId);
    }
}
