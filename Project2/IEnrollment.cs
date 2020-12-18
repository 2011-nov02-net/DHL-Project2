namespace Project2
{
    public interface IEnrollment
    {
        int User { get; set; }
        int Course { get; set; }
        int? Grade { get; set; }
        ICourse CourseNavigation { get; set; }
        IGrade GradeNavigation { get; set; }
        IUser UserNavigation { get; set; }
    }
}
