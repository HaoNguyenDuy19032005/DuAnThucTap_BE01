using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.Dtos
{
    public class TestAssignmentRequestDto : IValidatableObject
    {
        [Required(ErrorMessage = "ID Bài kiểm tra không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Bài kiểm tra không hợp lệ.")]
        public int Testid { get; set; }

        [Required(ErrorMessage = "ID Lớp không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Lớp không hợp lệ.")]
        public int Classid { get; set; }

        [StringLength(50, ErrorMessage = "Trạng thái không được vượt quá 50 ký tự.")]
        public string? Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Thêm các kiểm tra bổ sung nếu cần
            yield break;
        }
    }
}