using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataModel
{
    public partial class Instructor
    {
        public Instructor()
        {
            Classes = new HashSet<Class>();
            Departments = new HashSet<Department>();
        }

        public int DepartmentId { get; set; }
        public int InstructorId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Person InstructorNavigation { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
