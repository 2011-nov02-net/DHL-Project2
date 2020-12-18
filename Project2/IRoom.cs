using System.Collections.Generic;

namespace Project2
{
    public interface IRoom
    {
        int Id { get; set; }
        decimal Number { get; set; }
        int? Capacity { get; set; }
        int BuildingId { get; set; }
        IBuilding Building { get; set; }
        ICollection<IReservation> Reservations { get; set; }
    }
}
