using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataModel
{
    public partial class Permission
    {
        public Permission()
        {
            Users = new HashSet<User>();
        }

        public int Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
