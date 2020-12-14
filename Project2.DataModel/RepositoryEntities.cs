using System;

namespace Project2.DataModel
{
    // https://www.c-sharpcorner.com/article/generic-repository-pattern-in-asp-net-core/
    public class Class1
    {
    }
    class Entity<PkT> {
        PkT Id {get; set;}
    }
}
