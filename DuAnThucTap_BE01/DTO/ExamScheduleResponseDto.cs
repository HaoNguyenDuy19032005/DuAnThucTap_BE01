using System;

namespace DuAnThucTap_BE01.DTO
{
    public class ExamScheduleResponseDto
    {
        public int ExamScheduleId { get; set; }

        // From ExamSchedule
        public int ExamId { get; set; }
        public int ClassId { get; set; }

        // From related Exam
        public string? ExamName { get; set; }
        public DateOnly? ExamDate { get; set; }

        // From related Class
        public string? ClassName { get; set; }
        public string? SchoolYearName { get; set; } // Lấy từ Class -> SchoolYear
    }
}
