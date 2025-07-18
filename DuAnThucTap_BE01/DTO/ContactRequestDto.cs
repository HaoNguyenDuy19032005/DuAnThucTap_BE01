using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.DTO
{
    public class ContactRequestDto : IValidatableObject
    {
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ và tên không được vượt quá 100 ký tự")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Mối quan hệ không được để trống")]
        [StringLength(50, ErrorMessage = "Mối quan hệ không được vượt quá 50 ký tự")]
        public string Relationship { get; set; }

        [StringLength(255, ErrorMessage = "Địa chỉ không được vượt quá 255 ký tự")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(15, ErrorMessage = "Số điện thoại không được vượt quá 15 ký tự")]
        public string Phonenumber { get; set; }

        public int? Teacherid { get; set; }
        public int? Studentid { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Teacherid.HasValue && Studentid.HasValue)
            {
                yield return new ValidationResult(
                    "Một liên hệ không thể cùng lúc thuộc về cả Giáo viên và Học sinh.",
                    new[] { nameof(Teacherid), nameof(Studentid) });
            }

            if (!Teacherid.HasValue && !Studentid.HasValue)
            {
                yield return new ValidationResult(
                    "Cần phải cung cấp ID của Giáo viên hoặc Học sinh.",
                    new[] { nameof(Teacherid), nameof(Studentid) });
            }
        }
    }
}