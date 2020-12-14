using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataModel
{
    public partial class Transcript
    {
        public int PersonId { get; set; }
        public int CourseId { get; set; }
        public string Grade { get; set; }

        public virtual Class Course { get; set; }
        public virtual Person Person { get; set; }
    }
}
