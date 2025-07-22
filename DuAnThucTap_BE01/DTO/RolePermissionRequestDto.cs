using System.ComponentModel.DataAnnotations;
namespace DuAnThucTap_BE01.DTO
{
    public class RolePermissionRequestDto : IValidatableObject
    {
        [Required(ErrorMessage = "ID Vai trò không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Vai trò không hợp lệ.")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "ID Quyền không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Quyền không hợp lệ.")]
        public int PermissionId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
