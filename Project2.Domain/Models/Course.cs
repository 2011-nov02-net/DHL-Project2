using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Domain.Models
{
    public class Course
    {
        public int Id { get; set; }
        private string _name;
        public string Name 
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name cannot be empty or null");
                _name = value;
            }
        }
        public string Description { get; set; }
        private int _creditValue;
        public int CreditValue 
        { 
            get { return _creditValue; }
            set
            {
                if (value < 0) throw new ArgumentException("Credit value must be zero or higher");
                _creditValue = value;
            }
        }
        public int DepartmentId { get; set; }
        public int? Code { get; set; }
        public int? Session { get; set; }
        public int? Category { get; set; }
        public int? Capacity { get; set; }
        public int? WaitlistCapacity { get; set; }

        public Course(int id, string name, string description, int creditValue)
        {
            Id = id;
            Name = name;
            Description = description;
            CreditValue = creditValue;
        }
    }
}
