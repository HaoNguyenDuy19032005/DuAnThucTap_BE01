using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("permissions")]
    [Index("Permissionid", Name = "permissions_permissionid_key", IsUnique = true)]
    public partial class Permission
    {
        public Permission()
        {
            Rolepermissions = new HashSet<Rolepermission>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("permissionid")]
        [StringLength(255)]
        public string Permissionid { get; set; } = null!;
        [Column("module")]
        [StringLength(255)]
        public string Module { get; set; } = null!;
        [Column("permissioncode")]
        [StringLength(255)]
        public string Permissioncode { get; set; } = null!;
        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; } = null!;

        public virtual ICollection<Rolepermission> Rolepermissions { get; set; }
    }
}
