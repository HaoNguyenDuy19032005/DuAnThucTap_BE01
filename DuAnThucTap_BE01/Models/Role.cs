using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("roles")]
    [Index("RoleName", Name = "roles_rolename_key", IsUnique = true)]
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
            Permissions = new HashSet<Permission>();
            RolePermissions = new HashSet<RolePermission>();
            RoleName = string.Empty;
        }

        [Key]
        [Column("roleid")]
        public int RoleId { get; set; }

        [Column("rolename")]
        [StringLength(100)]
        public string RoleName { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [InverseProperty("Role")]
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }

        [InverseProperty("Roles")]
        [JsonIgnore]
        public virtual ICollection<Permission> Permissions { get; set; }

        [InverseProperty("Role")]
        [JsonIgnore]
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}