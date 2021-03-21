using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Domain.Models
{
    public class User
    {
        private int _id;
        public int Id 
        {
            get { return _id; } 
            set
            {
                if (value <= 0) throw new ArgumentException("User Id must be greater than zero");
                _id = value;
            }
        }
        private string _fullName;
        public string FullName 
        { 
            get { return _fullName; } 
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name cannot be empty or null.");
                _fullName = value;
            }
        }
        private string _email;
        public string Email 
        { 
            get { return _email; } 
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Email cannot be empty or null.");
                _email = value;
            }
        }

        public User(int id, string fullName, string email)
        {
            Id = id;
            FullName = fullName;
            Email = email;
        }
    }
}
