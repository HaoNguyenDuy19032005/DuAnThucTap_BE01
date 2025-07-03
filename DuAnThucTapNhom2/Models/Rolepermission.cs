using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("rolepermissions")]
    [Index("FkRoleid", "FkPermissionid", Name = "rolepermissions_fk_roleid_fk_permissionid_key", IsUnique = true)]
    public partial class Rolepermission
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("fk_roleid")]
        [StringLength(255)]
        public string FkRoleid { get; set; } = null!;
        [Column("fk_permissionid")]
        [StringLength(255)]
        public string FkPermissionid { get; set; } = null!;

        public virtual Permission FkPermission { get; set; } = null!;
        public virtual Role FkRole { get; set; } = null!;
    }
}
