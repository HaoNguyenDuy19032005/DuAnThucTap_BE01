// File: Dtos/LessonDto.cs
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.Dtos
{
    public class LessonDto
    {
        [DisplayName("ID Buổi học")]
        public int LessonId { get; set; }

        [DisplayName("Tiêu đề")]
        public string Title { get; set; } = null!;

        [DisplayName("Mô tả")]
        public string? Description { get; set; }

        [DisplayName("Thời gian bắt đầu")]
        public DateTime? StartTime { get; set; }

        [DisplayName("Thời gian kết thúc")]
        public DateTime? EndTime { get; set; }

        [DisplayName("Thời lượng (phút)")]
        public int? DurationInMinutes { get; set; }

        [DisplayName("Bật ghi hình")]
        public bool? IsRecordingEnabled { get; set; }

        [DisplayName("Cho phép học sinh chia sẻ")]
        public bool? AllowStudentSharing { get; set; }

        [DisplayName("Đường dẫn chia sẻ")]
        public string? ShareableLink { get; set; }

        [DisplayName("ID Phòng học")]
        public string? MeetingId { get; set; }

        [DisplayName("Ngày tạo")]
        public DateTime? CreatedAt { get; set; }

        // --- Dữ liệu từ các bảng liên quan (chỉ hiển thị tên) ---
        [DisplayName("Tên khóa học")]
        public string? CourseName { get; set; }

        [DisplayName("Tên giáo viên")]
        public string? TeacherName { get; set; }
    }
}