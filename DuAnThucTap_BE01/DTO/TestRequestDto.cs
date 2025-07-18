using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.Dtos
{
    public class TestRequestDto : IValidatableObject
    {
        [Required(ErrorMessage = "Tiêu đề không được để trống.")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Tiêu đề phải có độ dài từ 3 đến 255 ký tự.")]
        public string Title { get; set; } = null!;

        [StringLength(100, ErrorMessage = "Định dạng bài kiểm tra không được vượt quá 100 ký tự.")]
        public string? Testformat { get; set; }

        [Range(1, 1440, ErrorMessage = "Thời lượng phải từ 1 đến 1440 phút (24 giờ).")]
        public int? Durationinminutes { get; set; }

        [Required(ErrorMessage = "Thời gian bắt đầu không được để trống.")]
        [DataType(DataType.DateTime)]
        public DateTime? Starttime { get; set; }

        [Required(ErrorMessage = "Thời gian kết thúc không được để trống.")]
        [DataType(DataType.DateTime)]
        public DateTime? Endtime { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
        public string? Description { get; set; }

        [StringLength(100, ErrorMessage = "Phân loại không được vượt quá 100 ký tự.")]
        public string? Classification { get; set; }

        public bool Requirestudentattachment { get; set; }

        [Required(ErrorMessage = "ID Giáo viên không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Giáo viên không hợp lệ.")]
        public int Teacherid { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Starttime.HasValue && Endtime.HasValue)
            {
                if (Starttime.Value >= Endtime.Value)
                {
                    yield return new ValidationResult(
                        "Thời gian bắt đầu phải trước thời gian kết thúc.",
                        new[] { nameof(Starttime), nameof(Endtime) }
                    );
                }

                if (Starttime.Value < DateTime.UtcNow)
                {
                    yield return new ValidationResult(
                        "Thời gian bắt đầu không được ở quá khứ.",
                        new[] { nameof(Starttime) }
                    );
                }
            }
        }
    }
}