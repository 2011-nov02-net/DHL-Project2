using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.Api
{
    public partial class Person
    {
        public Person()
        {
            Enrollments = new HashSet<Enrollment>();
            Transcripts = new HashSet<Transcript>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public virtual Instructor Instructor { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Transcript> Transcripts { get; set; }
    }
}
