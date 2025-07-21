namespace Nhom2ThucTap.DTO
{
    public class DisplayedTestListDto
    {
        public int SubjectID { get; set; }
        public int TeacherID { get; set; }
        public string Title { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
