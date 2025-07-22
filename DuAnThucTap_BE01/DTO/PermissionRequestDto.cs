using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.DTO
{
    public class PermissionRequestDto : IValidatableObject
    {
        [Required(ErrorMessage = "Mã quyền không được để trống.")]
        [StringLength(100, ErrorMessage = "Mã quyền không được vượt quá 100 ký tự.")]
        public string PermissionCode { get; set; } = null!;

        [StringLength(100, ErrorMessage = "Mô-đun không được vượt quá 100 ký tự.")]
        public string? Module { get; set; }

        public string? Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(PermissionCode))
            {
                yield return new ValidationResult("Mã quyền không được để trống.", new[] { nameof(PermissionCode) });
            }
            yield break;
        }
    }
}