using System;

namespace DuAnThucTap_BE01.DTO
{
    public class ExamResponseDto
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; } = null!;
        public DateOnly? ExamDate { get; set; }
        public int? DurationMinutes { get; set; }
        public DateTime? CreatedAt { get; set; }

        // Dữ liệu từ các bảng liên quan
        public string? SchoolyearName { get; set; }
        public string? GradelevelName { get; set; }
        public string? SemesterName { get; set; }
        public string? SubjectName { get; set; }
    }
}
