using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.DTO
{
    public class PermissionDto
    {
        [DisplayName("ID Quyền")]
        public int PermissionID { get; set; }

        [DisplayName("Mô-đun")]
        public string? Module { get; set; }

        [DisplayName("Mã Quyền")]
        [Required(ErrorMessage = "Mã quyền không được để trống.")]
        public string PermissionCode { get; set; } = null!;

        [DisplayName("Mô tả")]
        public string? Description { get; set; }
    }
}