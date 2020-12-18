using System;
using System.Collections.Generic;

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

        public virtual Permission PermissionNavigation { get; set; }
        public virtual ICollection<CourseAssistant> CourseAssistants { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Waitlist> Waitlists { get; set; }
    }
}
