using System.Collections.Generic;

namespace Project2
{
    public interface IGrade
    {
        int Id { get; set; }
        string Letter { get; set; }
        int? Value { get; set; }
        ICollection<IEnrollment> Enrollments { get; set; }
    }
}
