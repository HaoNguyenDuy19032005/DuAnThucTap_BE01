using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.DTO
{
    public class TeacherWorkStatusHistoryRequestDto : IValidatableObject
    {
        [Required(ErrorMessage = "ID Giáo viên không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Giáo viên không hợp lệ.")]
        public int Teacherid { get; set; }

        [Required(ErrorMessage = "Loại trạng thái không được để trống.")]
        [StringLength(100, ErrorMessage = "Loại trạng thái không được vượt quá 100 ký tự.")]
        public string Statustype { get; set; } = string.Empty;

        // Đổi sang string?
        [Required(ErrorMessage = "Ngày bắt đầu không được để trống.")]
        public string? Startdate { get; set; }

        // Đổi sang string?
        public string? Enddate { get; set; }

        [StringLength(500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự.")]
        public string? Note { get; set; }

        [Url(ErrorMessage = "Đường dẫn tệp quyết định không hợp lệ.")]
        [StringLength(500, ErrorMessage = "Đường dẫn tệp quyết định không được vượt quá 500 ký tự.")]
        public string? Decisionfileurl { get; set; }

        // Cập nhật lại logic Validate để làm việc với string
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            DateOnly parsedStartDate;

            if (string.IsNullOrEmpty(Startdate) || !DateOnly.TryParse(Startdate, out parsedStartDate))
            {
                yield return new ValidationResult(
                    "Định dạng Ngày bắt đầu không hợp lệ, vui lòng nhập theo dạng yyyy-MM-dd.",
                    new[] { nameof(Startdate) }
                );
            }
            else
            {
                if (parsedStartDate > DateOnly.FromDateTime(DateTime.Today))
                {
                    yield return new ValidationResult(
                        "Ngày bắt đầu không được là một ngày trong tương lai.",
                        new[] { nameof(Startdate) }
                    );
                }
            }

            if (!string.IsNullOrEmpty(Enddate))
            {
                if (DateOnly.TryParse(Enddate, out DateOnly parsedEndDate))
                {
                    if (parsedStartDate >= parsedEndDate)
                    {
                        yield return new ValidationResult(
                            "Ngày kết thúc phải sau Ngày bắt đầu.",
                            new[] { nameof(Enddate) }
                        );
                    }
                }
                else
                {
                    yield return new ValidationResult(
                       "Định dạng Ngày kết thúc không hợp lệ, vui lòng nhập theo dạng yyyy-MM-dd.",
                       new[] { nameof(Enddate) }
                   );
                }
            }
        }
    }
}