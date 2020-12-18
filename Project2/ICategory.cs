using System.Collections.Generic;

namespace Project2
{
    public interface ICategory
    {
        int Id { get; set; }
        string Name { get; set; }
        ICollection<ICourse> Courses { get; set; }
    }
}
