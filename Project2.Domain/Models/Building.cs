using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Domain.Models
{
    public class Building
    {
        private int _id;
        public int Id 
        {
            get { return _id; } 
            set
            {
                if (value <= 0) throw new ArgumentException("Building Id must be greater than zero");
                _id = value;
            }
        }

        private string _name;
        public string Name 
        { 
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Building Name is required.");
                _name = value;
            }
        }

        public ICollection<Room> Rooms { get; set; } = null;

        public Building(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Building(int id, string name, ICollection<Room> rooms)
        {
            Id = id;
            Name = name;
            Rooms = rooms;
        }


    }
}
