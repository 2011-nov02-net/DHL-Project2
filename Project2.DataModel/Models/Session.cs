using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataModel
{
    public partial class Session
    {
        public Session()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
