using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.DTO
{
    public class PermissionRequestDto
    {
        [StringLength(100, ErrorMessage = "Module không được vượt quá 100 ký tự.")]
        public string? Module { get; set; }

        [Required(ErrorMessage = "Mã quyền không được để trống.")]
        [StringLength(100, ErrorMessage = "Mã quyền không được vượt quá 100 ký tự.")]
        public string Permissioncode { get; set; } = null!;

        public string? Description { get; set; }
    }
}
