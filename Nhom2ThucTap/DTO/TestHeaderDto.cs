using Microsoft.AspNetCore.Http;

namespace Nhom2ThucTap.DTO
{
    public class TestHeaderDto
    {
        public int SubjectId { get; set; }
        public int ClassId { get; set; }
        public string? Title { get; set; }
        public string? TestFormat { get; set; }
        public int DurationInMinutes { get; set; }
        public DateTime StartTime { get; set; }
        public bool RequireStudentAttachment { get; set; }
        public string? SubmissionRules { get; set; }

        public IFormFile? Attachment { get; set; }  // File đính kèm (ảnh)
    }
}
