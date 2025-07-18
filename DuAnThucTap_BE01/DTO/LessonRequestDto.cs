// File: Dtos/LessonRequestDto.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.Dtos
{
    public class LessonRequestDto : IValidatableObject
    {
        [Required(ErrorMessage = "ID Khóa học không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Khóa học không hợp lệ.")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "ID Giáo viên không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Giáo viên không hợp lệ.")]
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống.")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Tiêu đề phải có từ 5 đến 255 ký tự.")]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [Required(ErrorMessage = "Thời gian bắt đầu không được để trống.")]
        public DateTime? StartTime { get; set; }

        [Required(ErrorMessage = "Thời gian kết thúc không được để trống.")]
        public DateTime? EndTime { get; set; }

        [StringLength(50, ErrorMessage = "Mật khẩu không được vượt quá 50 ký tự.")]
        public string? Password { get; set; }

        public bool? AutoStartOnTime { get; set; }
        public bool? IsRecordingEnabled { get; set; }
        public bool? AllowStudentSharing { get; set; }

        // Phương thức Validate để kiểm tra các logic phức tạp
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // 1. Kiểm tra thời gian bắt đầu và kết thúc
            if (StartTime.HasValue && EndTime.HasValue)
            {
                if (StartTime.Value >= EndTime.Value)
                {
                    yield return new ValidationResult("Thời gian bắt đầu phải sớm hơn thời gian kết thúc.", new[] { nameof(StartTime), nameof(EndTime) });
                }

                if (StartTime.Value < DateTime.UtcNow)
                {
                    yield return new ValidationResult("Không thể tạo hoặc cập nhật buổi học trong quá khứ.", new[] { nameof(StartTime) });
                }

                var duration = (EndTime.Value - StartTime.Value).TotalMinutes;
                if (duration < 5)
                {
                    // SỬA Ở ĐÂY: Gán lỗi cho StartTime và EndTime
                    yield return new ValidationResult("Buổi học phải kéo dài ít nhất 5 phút.", new[] { nameof(StartTime), nameof(EndTime) });
                }
                if (duration > 480) // 8 tiếng
                {
                    // SỬA Ở ĐÂY: Gán lỗi cho StartTime và EndTime
                    yield return new ValidationResult("Buổi học không thể kéo dài quá 8 tiếng.", new[] { nameof(StartTime), nameof(EndTime) });
                }
            }
        }
    }
}