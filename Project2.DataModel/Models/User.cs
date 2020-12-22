using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Project2.DataModel
{
    public partial class User
    {
        public User()
        {
            CourseAssistants = new HashSet<CourseAssistant>();
            Departments = new HashSet<Department>();
            Enrollments = new HashSet<Enrollment>();
            Instructors = new HashSet<Instructor>();
            Waitlists = new HashSet<Waitlist>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int? Permission { get; set; }

        [JsonIgnore]
        public virtual Permission PermissionNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<CourseAssistant> CourseAssistants { get; set; }
        [JsonIgnore]
        public virtual ICollection<Department> Departments { get; set; }
        [JsonIgnore]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        [JsonIgnore]
        public virtual ICollection<Instructor> Instructors { get; set; }
        [JsonIgnore]
        public virtual ICollection<Waitlist> Waitlists { get; set; }
    }
}
