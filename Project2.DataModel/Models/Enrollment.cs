using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataModel
{
    public partial class Enrollment
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }

        public virtual Class Course { get; set; }
        public virtual Person Student { get; set; }
    }
}
