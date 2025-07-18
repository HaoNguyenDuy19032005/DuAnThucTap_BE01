using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

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

        // Thêm trường để nhận file đính kèm
        public IFormFile? AttachmentFile { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Starttime.HasValue && Endtime.HasValue)
            {
                // Kiểm tra Starttime < Endtime
                if (Starttime.Value >= Endtime.Value)
                {
                    yield return new ValidationResult(
                        "Thời gian bắt đầu phải trước thời gian kết thúc.",
                        new[] { nameof(Starttime), nameof(Endtime) }
                    );
                }

                // Kiểm tra Starttime không ở quá khứ
                if (Starttime.Value < DateTime.UtcNow)
                {
                    yield return new ValidationResult(
                        "Thời gian bắt đầu không được ở quá khứ.",
                        new[] { nameof(Starttime) }
                    );
                }

                // Kiểm tra Durationinminutes khớp với khoảng thời gian từ Starttime đến Endtime
                if (Durationinminutes.HasValue)
                {
                    var duration = (Endtime.Value - Starttime.Value).TotalMinutes;
                    if (Math.Abs(duration - Durationinminutes.Value) > 0.5) // Cho phép sai số 0.5 phút
                    {
                        yield return new ValidationResult(
                            "Khoảng thời gian từ Starttime đến Endtime phải bằng Durationinminutes.",
                            new[] { nameof(Durationinminutes), nameof(Starttime), nameof(Endtime) }
                        );
                    }
                }
            }

            // Kiểm tra định dạng file nếu có file đính kèm
            if (AttachmentFile != null)
            {
                var allowedExtensions = new[] { ".doc", ".docx", ".ppt", ".pptx", ".xls", ".xlsx", ".jpeg" };
                var extension = Path.GetExtension(AttachmentFile.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                {
                    yield return new ValidationResult(
                        "Chỉ chấp nhận các định dạng file: doc, docx, ppt, pptx, xls, xlsx, jpeg.",
                        new[] { nameof(AttachmentFile) }
                    );
                }

                // Kiểm tra kích thước file (50MB = 50 * 1024 * 1024 bytes)
                if (AttachmentFile.Length > 50 * 1024 * 1024)
                {
                    yield return new ValidationResult(
                        "Kích thước file không được vượt quá 50MB.",
                        new[] { nameof(AttachmentFile) }
                    );
                }
            }
        }
    }
}