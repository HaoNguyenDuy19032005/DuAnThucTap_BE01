using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.Dto
{
    public class ExamDto : IValidatableObject
    {
        // Không cần Examid ở đây vì nó sẽ được tạo tự động hoặc truyền qua URL

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
            ErrorMessage = "Tên kỳ thi chỉ được chứa chữ cái, số, khoảng trắng, dấu gạch ngang và gạch dưới.")]
        public string Examname { get; set; } = null!;

        [Required(ErrorMessage = "Ngày thi không được để trống.")]
        public DateOnly Examdate { get; set; }

        [Range(1, 1000, ErrorMessage = "Thời lượng thi phải từ 1 đến 1000 phút.")] // Đặt giới hạn trên hợp lý hơn
        public int? Durationminutes { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Mã loại lớp không hợp lệ.")]
        public int? Classtypeid { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Mã loại hình chấm thi không hợp lệ.")]
        public int? Graderassignmenttypeid { get; set; }

        // Custom validation logic
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Kiểm tra xem ngày thi có phải là ngày trong tương lai không
            if (Examdate > DateOnly.FromDateTime(DateTime.Now))
            {
                yield return new ValidationResult(
                    "Ngày thi không được là một ngày trong tương lai.",
                    new[] { nameof(Examdate) }
                );
            }

            // Các logic phức tạp khác có thể thêm ở đây
        }
    }
}
