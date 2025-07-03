using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("examgraders")]
    [Index("Examgraderid", Name = "examgraders_examgraderid_key", IsUnique = true)]
    public partial class Examgrader
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("examgraderid")]
        [StringLength(255)]
        public string Examgraderid { get; set; } = null!;
        [Column("fk_examscheduleid")]
        [StringLength(255)]
        public string FkExamscheduleid { get; set; } = null!;
        [Column("fk_teacherid")]
        [StringLength(255)]
        public string FkTeacherid { get; set; } = null!;

        public virtual Examschedule FkExamschedule { get; set; } = null!;
        public virtual Teacher FkTeacher { get; set; } = null!;
    }
}
