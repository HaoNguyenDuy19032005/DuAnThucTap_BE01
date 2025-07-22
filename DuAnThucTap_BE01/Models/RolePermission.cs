using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("rolepermissions")]
    public partial class RolePermission
    {
        [Key]
        [Column("roleid")]
        public int RoleId { get; set; }

        [Key]
        [Column("permissionid")]
        public int PermissionId { get; set; }

        [ForeignKey("RoleId")]
        [JsonIgnore]
        public virtual Role? Role { get; set; }

        [ForeignKey("PermissionId")]
        [JsonIgnore]
        public virtual Permission? Permission { get; set; }
    }
}