﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataModel
{
    public partial class Department
    {
        public Department()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? DeanId { get; set; }

        public virtual User Dean { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
