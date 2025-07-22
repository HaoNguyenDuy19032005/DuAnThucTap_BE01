using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.DTO
{
    public class RolePermissionRequestDto
    {
        [Required(ErrorMessage = "ID của vai trò không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID của vai trò không hợp lệ.")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "ID của quyền không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID của quyền không hợp lệ.")]
        public int PermissionId { get; set; }
    }
}
