using Microsoft.AspNetCore.Http;

namespace Nhom2ThucTap.DTO
{
    public class SchoolTransferHistoryCreateDto
    {
        public int? Studentid { get; set; }
        public int? Fromschoolinfoid { get; set; }
        public int? Toschoolinfoid { get; set; }
        public int? Semesterid { get; set; }
        public int? Fromclassid { get; set; }
        public int? Toclassid { get; set; }

        public string? Transferdate { get; set; }
        public string? Reason { get; set; }
        public string? Transfertype { get; set; }

        public IFormFile? Attachmentfile { get; set; }
    }
}
