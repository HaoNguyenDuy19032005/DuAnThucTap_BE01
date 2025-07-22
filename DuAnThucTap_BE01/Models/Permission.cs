using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuAnThucTap_BE01.Models
{
    [Table("permissions")]
    public partial class Permission
    {
        public Permission()
        {
            RolePermissions = new HashSet<RolePermission>();
        }

        [Key]
        [Column("permissionid")]
        public int Permissionid { get; set; }

        [Column("module")]
        [StringLength(100)]
        public string? Module { get; set; }

        [Column("permissioncode")]
        [StringLength(100)]
        public string Permissioncode { get; set; } = null!;

        [Column("description")]
        public string? Description { get; set; }

        // Sửa: Thay ICollection<Role> bằng ICollection<RolePermission>
        [InverseProperty("Permission")]
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}