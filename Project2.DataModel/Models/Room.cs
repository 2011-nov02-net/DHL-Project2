using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataModel
{
    public partial class Room
    {
        public Room()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public decimal Number { get; set; }
        public int? Capacity { get; set; }
        public int BuildingId { get; set; }

        public virtual Building Building { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
