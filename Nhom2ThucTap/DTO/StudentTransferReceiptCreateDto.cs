using Microsoft.AspNetCore.Http;

namespace Nhom2ThucTap.DTO
{
    public class StudentTransferReceiptCreateDto
    {
        public string? StudentName { get; set; }
        public string? StudentCode { get; set; }
        public string? TransferDate { get; set; } // yyyy-MM-dd
        public int? SemesterId { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? FromSchool { get; set; }
        public string? Reason { get; set; }
        public IFormFile? AttachmentFile { get; set; }
    }
}
