namespace Project2
{
    public interface ICourseAssistant
    {
        int AssistantId { get; set; }
        int CourseId { get; set; }
        string Role { get; set; }
        IUser Assistant { get; set; }
        ICourse Course { get; set; }
    }
}
