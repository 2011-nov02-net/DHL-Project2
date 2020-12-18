using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataModel
{
    public partial class Reservation
    {
        public int Room { get; set; }
        public int? CourseId { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan? End { get; set; }

        public virtual Course Course { get; set; }
        public virtual Room RoomNavigation { get; set; }
    }
}
