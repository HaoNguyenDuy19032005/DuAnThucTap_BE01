using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.DTO
{
    // 1. Thêm IValidatableObject để phương thức Validate() được tự động gọi
    public class TeacherTrainingHistoryRequestDto : IValidatableObject
    {
        [Required(ErrorMessage = "ID giáo viên không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID giáo viên không hợp lệ.")]
        public int Teacherid { get; set; }

        [Required(ErrorMessage = "Tên cơ sở đào tạo không được để trống.")]
        [StringLength(255, ErrorMessage = "Tên cơ sở đào tạo không được vượt quá 255 ký tự.")]
        public string Traininginstitutionname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Chuyên ngành không được để trống.")]
        [StringLength(255, ErrorMessage = "Chuyên ngành không được vượt quá 255 ký tự.")]
        public string Majororspecialization { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ngày bắt đầu không được để trống.")]
        public string Startdate { get; set; } = string.Empty; // 2. Bỏ [DataType] vì đã có custom validation

        [Required(ErrorMessage = "Ngày kết thúc/Năm tốt nghiệp không được để trống.")]
        [StringLength(50, ErrorMessage = "Ngày kết thúc/Năm tốt nghiệp không được vượt quá 50 ký tự.")]
        public string Enddateorgraduationyear { get; set; } = string.Empty;

        public bool Active { get; set; } = true;

        [Required(ErrorMessage = "Loại hình đào tạo không được để trống.")]
        [StringLength(100, ErrorMessage = "Loại hình đào tạo không được vượt quá 100 ký tự.")]
        public string Trainingtype { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên văn bằng/chứng chỉ không được để trống.")]
        [StringLength(255, ErrorMessage = "Tên văn bằng/chứng chỉ không được vượt quá 255 ký tự.")]
        public string Certificatediplomaname { get; set; } = string.Empty;

        [Url(ErrorMessage = "Đường dẫn tệp đính kèm không hợp lệ.")]
        [StringLength(500, ErrorMessage = "Đường dẫn tệp đính kèm không được vượt quá 500 ký tự.")]
        public string? Attachmenturl { get; set; }

        // 3. Sửa lại hoàn toàn phương thức Validate
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Kiểm tra Startdate
            if (!string.IsNullOrEmpty(Startdate))
            {
                // Chỉ kiểm tra khi chuỗi không rỗng
                if (DateOnly.TryParse(Startdate, out DateOnly parsedStartDate))
                {
                    // Nếu parse thành công, kiểm tra xem có phải ngày trong tương lai không
                    if (parsedStartDate > DateOnly.FromDateTime(DateTime.Today))
                    {
                        yield return new ValidationResult(
                            "Ngày bắt đầu không được là một ngày trong tương lai.",
                            new[] { nameof(Startdate) }
                        );
                    }
                }
                else
                {
                    // Nếu parse thất bại, trả về lỗi định dạng
                    yield return new ValidationResult(
                        "Định dạng ngày bắt đầu không hợp lệ, vui lòng nhập theo dạng yyyy-MM-dd.",
                        new[] { nameof(Startdate) }
                    );
                }
            }
        }
    }
}