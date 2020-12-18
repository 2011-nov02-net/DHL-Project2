using System.Collections.Generic;

namespace Project2.DataModel
{
    // https://www.c-sharpcorner.com/article/generic-repository-pattern-in-asp-net-core/
    public abstract class EntityBase {}
    public interface IEntity<PkT> {
        public PkT Id {get; set;}
    }
    partial class Course : EntityBase, IEntity<int>, ICourse
    {
        ICategory ICourse.CategoryNavigation 
        { 
            get => CategoryNavigation as ICategory;
            set => CategoryNavigation = value as Category; 
        }
        IDepartment ICourse.Department 
        { 
            get => Department as IDepartment; 
            set => Department = value as Department; 
        }
        ISession ICourse.SessionNavigation 
        { 
            get => SessionNavigation as ISession; 
            set => SessionNavigation = value as Session; 
        }
        ICollection<ICourseAssistant> ICourse.CourseAssistants 
        { 
            get => CourseAssistants as ICollection<ICourseAssistant>; 
            set => CourseAssistants = value as ICollection<CourseAssistant>; 
        }
        ICollection<IEnrollment> ICourse.Enrollments 
        { 
            get => Enrollments as ICollection<IEnrollment>; 
            set => Enrollments = value as ICollection<Enrollment>;
        }
        ICollection<IInstructor> ICourse.Instructors 
        { 
            get => Instructors as ICollection<IInstructor>; 
            set => Instructors = value as ICollection<Instructor>;
        }
        ICollection<IReservation> ICourse.Reservations 
        {
            get => Reservations as ICollection<IReservation>;
            set => Reservations = value as ICollection<Reservation>;
        }
        ICollection<IWaitlist> ICourse.Waitlists 
        { 
            get => Waitlists as ICollection<IWaitlist>; 
            set => Waitlists = value as ICollection<Waitlist>; 
        }
    }
    partial class User : EntityBase, IEntity<int>, IUser
    {
        IPermission IUser.PermissionNavigation 
        { 
            get => PermissionNavigation as IPermission; 
            set => PermissionNavigation = value as Permission; 
        }
        ICollection<ICourseAssistant> IUser.CourseAssistants 
        { 
            get => CourseAssistants as ICollection<ICourseAssistant>; 
            set => CourseAssistants = value as ICollection<CourseAssistant>; 
        }
        ICollection<IDepartment> IUser.Departments 
        { 
            get => Departments as ICollection<IDepartment>; 
            set => Departments = value as ICollection<Department>; 
        }
        ICollection<IEnrollment> IUser.Enrollments 
        { 
            get => Enrollments as ICollection<IEnrollment>; 
            set => Enrollments = value as ICollection<Enrollment>;  
        }
        ICollection<IInstructor> IUser.Instructors 
        { 
            get => Instructors as ICollection<IInstructor>; 
            set => Instructors = value as ICollection<Instructor>; 
        }
        ICollection<IWaitlist> IUser.Waitlists 
        { 
            get => Waitlists as ICollection<IWaitlist>; 
            set => Waitlists = value as ICollection<Waitlist>; 
        }
    }
    partial class Building : EntityBase, IEntity<int>, IBuilding
    {
        ICollection<IRoom> IBuilding.Rooms 
        { 
            get => Rooms as ICollection<IRoom>; 
            set => Rooms = value as ICollection<Room>;  
        }
    }
    partial class Department : EntityBase, IEntity<int>, IDepartment
    {
        IUser IDepartment.Dean 
        { 
            get => Dean; 
            set => Dean = value as User; 
        }
        ICollection<ICourse> IDepartment.Courses 
        { 
            get => Courses as ICollection<ICourse>; 
            set => Courses = value as ICollection<Course>; 
        }
    }
    partial class Category : EntityBase, IEntity<int>, ICategory
    {
        ICollection<ICourse> ICategory.Courses 
        { 
            get => Courses as ICollection<ICourse>; 
            set => Courses = value as ICollection<Course>;  
        }
    }
    partial class Room : EntityBase, IEntity<int>, IRoom
    {
        IBuilding IRoom.Building 
        { 
            get => Building; 
            set => Building = value as Building;
        }
        ICollection<IReservation> IRoom.Reservations 
        { 
            get => Reservations as ICollection<IReservation>; 
            set => Reservations = value as ICollection<Reservation>;
        }
    }
    partial class Session : EntityBase, IEntity<int>, ISession
    {
        ICollection<ICourse> ISession.Courses 
        { 
            get => Courses as ICollection<ICourse>; 
            set => Courses =  value as ICollection<Course>; 
        }
    }
    partial class Grade : EntityBase, IEntity<int>, IGrade
    {
        ICollection<IEnrollment> IGrade.Enrollments 
        { 
            get => Enrollments as ICollection<IEnrollment>;
            set => Enrollments = value as ICollection<Enrollment>;
        }
    }
    partial class Enrollment : IEnrollment
    {
        ICourse IEnrollment.CourseNavigation 
        { 
            get => CourseNavigation; 
            set => CourseNavigation = value as Course; 
        }
        IGrade IEnrollment.GradeNavigation 
        { 
            get => GradeNavigation; 
            set => GradeNavigation = value as Grade;
        }
        IUser IEnrollment.UserNavigation 
        { 
            get => UserNavigation; 
            set => UserNavigation = value as User; 
        }
    }
    partial class Reservation : IReservation
    {
        ICourse IReservation.Course 
        { 
            get => Course; 
            set => Course = value as Course; 
        }
        IRoom IReservation.RoomNavigation 
        { 
            get => RoomNavigation; 
            set => RoomNavigation = value as Room; 
        }
    }
    partial class CourseAssistant : ICourseAssistant
    {
        IUser ICourseAssistant.Assistant 
        { 
            get => Assistant; 
            set => Assistant = value as User; 
        }
        ICourse ICourseAssistant.Course 
        { 
            get => Course; 
            set => Course = value as Course; 
        }
    }
    partial class Instructor : IInstructor
    {
        ICourse IInstructor.Course 
        { 
            get => Course; 
            set => Course = value as Course; 
        }
        IUser IInstructor.InstructorNavigation 
        { 
            get => InstructorNavigation;
            set => InstructorNavigation = value as User; 
        }
    }
    partial class Waitlist : IWaitlist
    {
        ICourse IWaitlist.Course 
        { 
            get => Course; 
            set => Course = value as Course; 
        }
        IUser IWaitlist.UserNavigation 
        { 
            get => UserNavigation; 
            set => UserNavigation = value as User; 
        }
    }
    partial class Permission : IPermission
    {
        ICollection<IUser> IPermission.Users 
        { 
            get => Users as ICollection<IUser>;
            set => Users = value as ICollection<User>; 
        }
    }
}