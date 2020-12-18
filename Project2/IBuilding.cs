using System.Collections.Generic;

namespace Project2
{
    public interface IBuilding
    {
        int Id { get; set; }
        string Name { get; set; }
        ICollection<IRoom> Rooms { get; set; }
    }
}
