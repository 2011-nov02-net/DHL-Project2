using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataModel
{
    public partial class Class
    {
        public Class()
        {
            Enrollments = new HashSet<Enrollment>();
            Transcripts = new HashSet<Transcript>();
        }

        public int Id { get; set; }
        public int InstructorId { get; set; }
        public int DepartmentId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public int CourseCapacity { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int BuildingId { get; set; }
        public string RoomNumber { get; set; }

        public virtual Building Building { get; set; }
        public virtual Department Department { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Transcript> Transcripts { get; set; }
    }
}
