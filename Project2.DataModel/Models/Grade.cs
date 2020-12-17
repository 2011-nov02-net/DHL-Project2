using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataModel
{
    public partial class Grade
    {
        public Grade()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        public int Id { get; set; }
        public string Letter { get; set; }
        public int? Value { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
