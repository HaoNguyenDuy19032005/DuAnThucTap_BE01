using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("systemsettings")]
    [Index("Settingkey", Name = "systemsettings_settingkey_key", IsUnique = true)]
    public partial class Systemsetting
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("settingkey")]
        [StringLength(255)]
        public string Settingkey { get; set; } = null!;
        [Column("settingvalue")]
        [StringLength(255)]
        public string Settingvalue { get; set; } = null!;
        [Column("fk_userid")]
        [StringLength(255)]
        public string? FkUserid { get; set; }

        public virtual User? FkUser { get; set; }
    }
}
