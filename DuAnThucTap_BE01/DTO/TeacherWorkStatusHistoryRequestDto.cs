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

        [Required(ErrorMessage = "Ngày bắt đầu không được để trống.")]
        public DateOnly Startdate { get; set; }

        public DateOnly? Enddate { get; set; }

        [StringLength(500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự.")]
        public string? Note { get; set; }

        [Url(ErrorMessage = "Đường dẫn tệp quyết định không hợp lệ.")]
        [StringLength(500, ErrorMessage = "Đường dẫn tệp quyết định không được vượt quá 500 ký tự.")]
        public string? Decisionfileurl { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Startdate > DateOnly.FromDateTime(DateTime.Today))
            {
                yield return new ValidationResult(
                    "Ngày bắt đầu không được là một ngày trong tương lai.",
                    new[] { nameof(Startdate) }
                );
            }

            if (Enddate.HasValue && Startdate >= Enddate.Value)
            {
                yield return new ValidationResult(
                    "Ngày kết thúc phải sau ngày bắt đầu.",
                    new[] { nameof(Enddate) }
                );
            }
        }
    }
}
