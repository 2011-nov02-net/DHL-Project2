using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataModel
{
    public partial class Building
    {
        public Building()
        {
            Classes = new HashSet<Class>();
        }

        public int Id { get; set; }
        public string BuildingName { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
