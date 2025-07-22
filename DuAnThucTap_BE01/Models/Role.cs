using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuAnThucTap_BE01.Models
{
    [Table("roles")]
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
            RolePermissions = new HashSet<RolePermission>();
        }

        [Key]
        [Column("roleid")]
        public int Roleid { get; set; }

        [Column("rolename")]
        [StringLength(100)]
        public string Rolename { get; set; } = null!;

        [Column("description")]
        public string? Description { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<User> Users { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}