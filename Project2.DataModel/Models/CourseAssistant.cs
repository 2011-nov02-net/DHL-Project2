using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataModel
{
    public partial class CourseAssistant
    {
        public int AssistantId { get; set; }
        public int CourseId { get; set; }
        public string Role { get; set; }

        public virtual User Assistant { get; set; }
        public virtual Course Course { get; set; }
    }
}
