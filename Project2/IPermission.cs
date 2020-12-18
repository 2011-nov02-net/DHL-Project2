using System.Collections.Generic;

namespace Project2
{
    public interface IPermission
    {
        int Code { get; set; }
        string Name { get; set; }
        ICollection<IUser> Users { get; set; }
    }
}
