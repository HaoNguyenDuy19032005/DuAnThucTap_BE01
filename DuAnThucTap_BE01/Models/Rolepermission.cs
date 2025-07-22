using System.ComponentModel.DataAnnotations.Schema;

namespace DuAnThucTap_BE01.Models
{
    [Table("rolepermissions")]
    public partial class RolePermission
    {
        [Column("roleid")]
        public int Roleid { get; set; }

        [Column("permissionid")]
        public int Permissionid { get; set; }

        [ForeignKey("Roleid")]
        [InverseProperty("RolePermissions")]
        public virtual Role Role { get; set; } = null!;

        [ForeignKey("Permissionid")]
        [InverseProperty("RolePermissions")]
        public virtual Permission Permission { get; set; } = null!;
    }
}
