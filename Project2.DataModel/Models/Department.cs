using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataModel
{
    public partial class Department
    {
        public Department()
        {
            Classes = new HashSet<Class>();
            Instructors = new HashSet<Instructor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int DeanId { get; set; }

        public virtual Instructor Dean { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
    }
}
