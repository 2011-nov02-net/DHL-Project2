using System;

namespace Project2
{
    public interface IReservation
    {
        int Room { get; set; }
        int? CourseId { get; set; }
        TimeSpan Start { get; set; }
        TimeSpan? End { get; set; }
        ICourse Course { get; set; }
        IRoom RoomNavigation { get; set; }
    }
}
