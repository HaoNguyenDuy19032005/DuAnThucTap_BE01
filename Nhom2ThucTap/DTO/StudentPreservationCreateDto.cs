using Microsoft.AspNetCore.Http;

namespace Nhom2ThucTap.DTO
{
    public class StudentPreservationCreateDto
    {
        public int? Studentid { get; set; }
        public int? Classid { get; set; }
        public int? Semesterid { get; set; }

        public string? Preservationdate { get; set; }

        public string? Preservationduration { get; set; }
        public string? Reason { get; set; }

        public IFormFile? Attachmentfile { get; set; }
    }
}
