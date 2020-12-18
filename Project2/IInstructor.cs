namespace Project2
{
    public interface IInstructor
    {
        int InstructorId { get; set; }
        int CourseId { get; set; }
        ICourse Course { get; set; }
        IUser InstructorNavigation { get; set; }
    }
}
