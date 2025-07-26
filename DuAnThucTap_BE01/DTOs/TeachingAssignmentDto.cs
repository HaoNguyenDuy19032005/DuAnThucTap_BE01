namespace DuAnThucTap_BE01.DTOs
{
    public class TeachingAssignmentDto
    {
        public int Assignmentid { get; set; } // Giữ lại ID chính của bản ghi

        // Chỉ giữ lại các trường tên, xóa các trường ID
        public string? TeacherName { get; set; }
        public string? SubjectName { get; set; }
        public string? ClasstypeName { get; set; }
        public string? TopicName { get; set; }
        public string? SchoolyearName { get; set; }

        // Các trường thông tin khác giữ nguyên
        public DateOnly? Teachingstartdate { get; set; }
        public DateOnly? Teachingenddate { get; set; }
        public string? Notes { get; set; }
    }
}