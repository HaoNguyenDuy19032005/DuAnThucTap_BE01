namespace DuAnThucTap_BE01.Models
{
    public class Tests
    {
        public int TestId { get; set; }
        public int TeacherId { get; set; }
        public string Title { get; set; } = null!;
        public string TestFormat { get; set; } = null!;
        public int DurationInMinutes { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; } = null!;
        public string Classification { get; set; } = null!;
        public string AttachmentUrl { get; set; } = null!;
        public bool RequireStudentAttachment { get; set; }
    }
}