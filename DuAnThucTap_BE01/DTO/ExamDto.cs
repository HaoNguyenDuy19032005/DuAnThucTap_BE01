using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization; // Although not strictly needed for DTOs, it's good practice if you anticipate serialization

namespace DuAnThucTap_BE01.DTO
{
    public class ExamDto : IValidatableObject
    {
        // No Examid here as it would be generated or passed via URL for Update/Delete operations

        [Required(ErrorMessage = "Mã năm học không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "Mã năm học không hợp lệ.")]
        public int Schoolyearid { get; set; }

        [Required(ErrorMessage = "Mã khối lớp không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "Mã khối lớp không hợp lệ.")]
        public int Gradelevelid { get; set; }

        [Required(ErrorMessage = "Mã học kỳ không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "Mã học kỳ không hợp lệ.")]
        public int Semesterid { get; set; }

        [Required(ErrorMessage = "Mã môn học không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "Mã môn học không hợp lệ.")]
        public int Subjectid { get; set; }

        [Required(ErrorMessage = "Tên kỳ thi không được để trống.")]
        [StringLength(255, ErrorMessage = "Tên kỳ thi không được vượt quá 255 ký tự.")]
        [RegularExpression(@"^[a-zA-Z0-9\sáÁàÀảẢãÃạẠăĂằẰẳẲẵẴặẶâÂầẦẩẨẫẪậẬéÉèÈẻẺẽẼẹẸêÊềỀểỂễỄệỆíÍìÌỉỈĩĨịỊóÓòÒỏỎõÕọỌôÔồỒổỔỗỖộỘơƠờỜởỞỡỠợỢúÚùÙủỦũŨụỤưƯừỪửỬữỮựỰýÝỳỲỷỶỹỸỵỴĐđ_-]+$",
            ErrorMessage = "Tên kỳ thi không được chứa ký tự đặc biệt (chỉ cho phép chữ cái, số, khoảng trắng, dấu gạch ngang và gạch dưới).")]
        public string Examname { get; set; } = null!;

        [Required(ErrorMessage = "Ngày thi không được để trống.")]
        public DateOnly Examdate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Thời lượng thi phải là số phút dương.")]
        public int? Durationminutes { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Mã loại lớp không hợp lệ.")]
        public int? Classtypeid { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Mã loại hình chấm thi không hợp lệ.")]
        public int? Graderassignmenttypeid { get; set; }

        // Createdat thường được hệ thống tự động thiết lập, không phải do client truyền lên khi tạo mới
        // public DateTime? Createdat { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Examdate > DateOnly.FromDateTime(DateTime.Today))
            {
                yield return new ValidationResult(
                    "Ngày thi không được là một ngày trong tương lai.",
                    new[] { nameof(Examdate) }
                );
            }

            // Bạn có thể thêm các quy tắc xác thực phức tạp hơn ở đây nếu cần
            // Ví dụ: kiểm tra xem Durationminutes có phải là một giá trị hợp lý không.
            if (Durationminutes.HasValue && Durationminutes <= 0)
            {
                yield return new ValidationResult(
                    "Thời lượng thi phải lớn hơn 0 phút.",
                    new[] { nameof(Durationminutes) }
                );
            }
        }
    }
}