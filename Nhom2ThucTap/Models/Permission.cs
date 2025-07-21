using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhom2ThucTap.Models
{
    [Table("permissions")]
    [Index("Permissioncode", Name = "permissions_permissioncode_key", IsUnique = true)]
    public partial class Permission
    {
        public Permission()
        {
            Roles = new HashSet<Role>();
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

        [ForeignKey("Permissionid")]
        [InverseProperty("Permissions")]
        public virtual ICollection<Role> Roles { get; set; }
    }
}
