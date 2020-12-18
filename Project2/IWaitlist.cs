using System;

namespace Project2
{
    public interface IWaitlist
    {
        int User { get; set; }
        int CourseId { get; set; }
        DateTime? Added { get; set; }
        ICourse Course { get; set; }
        IUser UserNavigation { get; set; }
    }
}
