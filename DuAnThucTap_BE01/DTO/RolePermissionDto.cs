using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace DuAnThucTap_BE01.DTO
{
    public class RolePermissionDto
    {
        [DisplayName("ID Quyền")]
        public int PermissionId { get; set; }

        [DisplayName("Module")]
        public string? Module { get; set; }

        [DisplayName("Mã Quyền")]
        public string PermissionCode { get; set; } = null!;

        [DisplayName("Mô tả")]
        public string? Description { get; set; }
    }
}
