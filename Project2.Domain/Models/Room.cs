using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Domain.Models
{
    public class Room
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (value <= 0) throw new ArgumentException("Room Id must be greater than zero");
                _id = value;
            }
        }

        private decimal _number;
        public decimal Number
        {
            get { return _number; }
            set
            {
                if (value <= 0 || value > 99999) throw new ArgumentException("Room number must be greater than zero but be at most five digits");
                _number = value;
            }
        }

        private int _capacity;
        public int Capacity
        {
            get { return _capacity; }
            set
            {
                if (value <= 0) throw new ArgumentException("Capacity must be greater than zero");
                _capacity = value;
            }
        }

        private int _buildingId;
        public int BuildingId
        {
            get { return _buildingId; }
            set
            {
                if (value <= 0) throw new ArgumentException("Building Id Value must be positive");
                _buildingId = value;
            }
        }

        public Building Building { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = null;

        public Room(int id, decimal number, int capacity, int buildingId, Building building)
        {
            Id = id;
            Number = number;
            Capacity = capacity;
            BuildingId = buildingId;
            Building = building;
        }

        public Room(int id, decimal number, int capacity, int buildingId, Building building, ICollection<Reservation> reservations)
        {
            Id = id;
            Number = number;
            Capacity = capacity;
            BuildingId = buildingId;
            Building = building;
            Reservations = reservations;
        }

        public Room(int id, decimal number, int capacity, int buildingId)
        {
            Id = id;
            Number = number;
            Capacity = capacity;
            BuildingId = buildingId;
        }

        public Room(int id, decimal number, int capacity, int buildingId, ICollection<Reservation> reservations)
        {
            Id = id;
            Number = number;
            Capacity = capacity;
            BuildingId = buildingId;
            Reservations = reservations;
        }

    }
}
