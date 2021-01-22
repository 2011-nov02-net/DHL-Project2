using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Domain.Models
{
    public class Reservation
    {
        private int _roomId;
        public int RoomId
        {
            get { return _roomId; }
            set
            {
                if (value <= 0) throw new ArgumentException("Room Id must be greater than zero");
                _roomId = value;
            }
        }

        public int CourseId { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Course Course { get; set; }
    }
}
