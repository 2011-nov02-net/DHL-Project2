using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Domain.Models
{
    class Department
    {
        private int _id;
        public int Id 
        {
            get { return _id; } 
            set
            {
                if (value <= 0) throw new ArgumentException("Department Id must be greater than zero");
                _id = value;
            }
        }
        private string _name;
        public string Name 
        { 
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Department name must exist");
                _name = value;
            }
        }
        private int _deanId;
        public int DeanId 
        {
            get { return _deanId; }
            set
            {
                if (value <= 0) throw new ArgumentException("Department Id must be greater than zero");
                _deanId = value;
            }
        }

        public User Dean { get; set; } = null;
        public virtual ICollection<Course> Courses { get; set; } = null;

        public Department(int id, string name, int deanId)
        {
            Id = id;
            Name = name;
            DeanId = deanId;
        }
        public Department(int id, string name, int deanId, User dean)
        {
            Id = id;
            Name = name;
            DeanId = deanId;
            Dean = dean;
        }
        public Department(int id, string name, int deanId, ICollection<Course> courses)
        {
            Id = id;
            Name = name;
            DeanId = deanId;
            Courses = courses;
        }

        public Department(int id, string name, int deanId, User dean, ICollection<Course> courses)
        {
            Id = id;
            Name = name;
            DeanId = deanId;
            Dean = dean;
            Courses = courses;
        }
    }
}
