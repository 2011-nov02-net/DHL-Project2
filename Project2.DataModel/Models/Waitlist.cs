using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataModel
{
    public partial class Waitlist
    {
        public int User { get; set; }
        public int CourseId { get; set; }
        public DateTime? Added { get; set; }

        public virtual Course Course { get; set; }
        public virtual User UserNavigation { get; set; }
    }
}
