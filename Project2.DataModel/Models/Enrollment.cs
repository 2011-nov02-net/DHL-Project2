using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataModel
{
    public partial class Enrollment
    {
        public int User { get; set; }
        public int Course { get; set; }
        public int? Grade { get; set; }

        public virtual Course CourseNavigation { get; set; }
        public virtual Grade GradeNavigation { get; set; }
        public virtual User UserNavigation { get; set; }
    }
}
