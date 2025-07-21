using Microsoft.AspNetCore.Http;

namespace Nhom2ThucTap.DTO
{
    public class StudentCommendationCreateDto
    {
        public int? Studentid { get; set; }
        public int? Semesterid { get; set; }
        public int? Schoolinfoid { get; set; }
        public int? Commendationtypeid { get; set; }
        public string? Commendationdate { get; set; }
        public string? Content { get; set; }

        public IFormFile? Attachmentfile { get; set; }
    }
}
