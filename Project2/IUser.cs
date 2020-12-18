using System.Collections.Generic;

namespace Project2
{
    public interface IUser
    {
        int Id { get; set; }
        string FullName { get; set; }
        string Email { get; set; }
        int? Permission { get; set; }
        IPermission PermissionNavigation { get; set; }
        ICollection<ICourseAssistant> CourseAssistants { get; set; }
        ICollection<IDepartment> Departments { get; set; }
        ICollection<IEnrollment> Enrollments { get; set; }
        ICollection<IInstructor> Instructors { get; set; }
        ICollection<IWaitlist> Waitlists { get; set; }
    }
}
