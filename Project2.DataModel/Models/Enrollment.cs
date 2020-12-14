using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.Api
{
    public partial class Enrollment
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }

        public virtual Class Course { get; set; }
        public virtual Person Student { get; set; }
    }
}
