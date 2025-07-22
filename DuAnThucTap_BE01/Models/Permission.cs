using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("Permissions")]
    public class Permission
    {
        [Key]
        [Column("PermissionID")]
        public int PermissionID { get; set; }

        [Column("Module")]
        [StringLength(100)]
        public string? Module { get; set; }

        [Column("PermissionCode")]
        [StringLength(100)]
        [Required]
        public string PermissionCode { get; set; } = null!;

        [Column("Description")]
        public string? Description { get; set; }

        [InverseProperty("Permissions")]
        [JsonIgnore]
        public virtual ICollection<Role> Roles { get; set; } = new HashSet<Role>();
    }
}