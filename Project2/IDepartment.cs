using System.Collections.Generic;

namespace Project2
{
    public interface IDepartment
    {
        int Id { get; set; }
        string Name { get; set; }
        int? DeanId { get; set; }
        IUser Dean { get; set; }
        ICollection<ICourse> Courses { get; set; }
    }
}
