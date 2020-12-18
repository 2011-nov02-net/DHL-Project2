using System;
using System.Collections.Generic;

namespace Project2
{
    public interface ISession
    {
        int Id { get; set; }
        string Name { get; set; }
        DateTime Start { get; set; }
        DateTime End { get; set; }
        ICollection<ICourse> Courses { get; set; }
    }
}
