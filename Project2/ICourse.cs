using System.Collections.Generic;

namespace Project2
{
    public interface ICourse
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int? CreditValue { get; set; }
        int? DepartmentId { get; set; }
        int? Code { get; set; }
        int? Session { get; set; }
        int? Category { get; set; }
        int? Capacity { get; set; }
        int? WaitlistCapacity { get; set; }
        ICategory CategoryNavigation { get; set; }
        IDepartment Department { get; set; }
        ISession SessionNavigation { get; set; }
        ICollection<ICourseAssistant> CourseAssistants { get; set; }
        ICollection<IEnrollment> Enrollments { get; set; }
        ICollection<IInstructor> Instructors { get; set; }
        ICollection<IReservation> Reservations { get; set; }
        ICollection<IWaitlist> Waitlists { get; set; }
    }
}
