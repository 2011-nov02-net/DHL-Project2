using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Project2.DataModel
{
    public partial class Course
    {
        public Course()
        {
            CourseAssistants = new HashSet<CourseAssistant>();
            Enrollments = new HashSet<Enrollment>();
            Instructors = new HashSet<Instructor>();
            Reservations = new HashSet<Reservation>();
            Waitlists = new HashSet<Waitlist>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CreditValue { get; set; }
        public int? DepartmentId { get; set; }
        public int? Code { get; set; }
        public int? Session { get; set; }
        public int? Category { get; set; }
        public int? Capacity { get; set; }
        public int? WaitlistCapacity { get; set; }

        [JsonIgnore]
        public virtual Category CategoryNavigation { get; set; }
        [JsonIgnore]
        public virtual Department Department { get; set; }
        [JsonIgnore]
        public virtual Session SessionNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<CourseAssistant> CourseAssistants { get; set; }
        [JsonIgnore]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        [JsonIgnore]
        public virtual ICollection<Instructor> Instructors { get; set; }
        [JsonIgnore]
        public virtual ICollection<Reservation> Reservations { get; set; }
        [JsonIgnore]
        public virtual ICollection<Waitlist> Waitlists { get; set; }
    }
}
