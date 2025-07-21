namespace Nhom2ThucTap.DTOs
{
    public class TestStudentSubmissionDto
    {
        public int? TestId { get; set; }
        public int? StudentId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? SubmissionTime { get; set; }
        public string? Status { get; set; }
    }
}
