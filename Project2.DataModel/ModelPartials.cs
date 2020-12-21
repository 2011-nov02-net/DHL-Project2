using System.Collections.Generic;

namespace Project2.DataModel
{
    // https://www.c-sharpcorner.com/article/generic-repository-pattern-in-asp-net-core/
    public abstract class EntityBase {}
    public interface IEntity<PkT> {
        public PkT Id {get; set;}
    }
    partial class Course : EntityBase, IEntity<int> {}
    partial class User : EntityBase, IEntity<int> {}
    partial class Building : EntityBase, IEntity<int> {}
    partial class Department : EntityBase, IEntity<int> {}
    partial class Category : EntityBase, IEntity<int> {}
    partial class Room : EntityBase, IEntity<int> {}
    partial class Session : EntityBase, IEntity<int> {}
    partial class Grade : EntityBase, IEntity<int> {}
}