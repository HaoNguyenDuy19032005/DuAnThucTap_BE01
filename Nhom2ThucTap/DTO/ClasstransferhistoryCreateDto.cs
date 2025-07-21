namespace Nhom2ThucTap.DTO
{
    public class ClasstransferhistoryCreateDto
    {
        public int? Studentid { get; set; }
        public int? Fromclassid { get; set; }
        public int? Toclassid { get; set; }
        public int? Semesterid { get; set; }
        public string? Reason { get; set; }
        public IFormFile? Attachmentfile { get; set; } 
    }
}