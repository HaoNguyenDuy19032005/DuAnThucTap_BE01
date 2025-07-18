using System;

namespace DuAnThucTap_BE01.DTO
{
    public class ExamGraderResponseDto
    {
        public int ExamGraderId { get; set; }

        // From Examgrader
        public int? ExamScheduleId { get; set; }
        public int? TeacherId { get; set; }

        // From related Teacher
        public string? TeacherName { get; set; }
        public string? TeacherCode { get; set; }

        // From related ExamSchedule -> Exam
        public string? ExamName { get; set; }
        public DateOnly? ExamDate { get; set; }

        // From related ExamSchedule -> Class
        public string? ClassName { get; set; }
    }
}
