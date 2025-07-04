using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("examgraders")]
    [Index("Examscheduleid", "Teacherid", Name = "examgraders_examscheduleid_teacherid_key", IsUnique = true)]
    public partial class Examgrader
    {
        [Key]
        [Column("examgraderid")]
        public Guid Examgraderid { get; set; }
        [Column("examscheduleid")]
        public Guid Examscheduleid { get; set; }
        [Column("teacherid")]
        public Guid Teacherid { get; set; }

        [ForeignKey("Examscheduleid")]
        [InverseProperty("Examgraders")]
        public virtual Examschedule Examschedule { get; set; } = null!;
        [ForeignKey("Teacherid")]
        [InverseProperty("Examgraders")]
        public virtual Teacher Teacher { get; set; } = null!;
    }
}
