using Microsoft.AspNetCore.Http;

namespace Nhom2ThucTap.DTO
{
    public class StudentDisciplineCreateDto
    {
        public int? Studentid { get; set; }
        public int? Semesterid { get; set; }
        public int? Schoolinfoid { get; set; }
        public int? Disciplinetypeid { get; set; }

        public string? Commendationdate { get; set; } // yyyy-MM-dd
        public string? Content { get; set; }
        public IFormFile? Attachmentfile { get; set; }
    }
}
