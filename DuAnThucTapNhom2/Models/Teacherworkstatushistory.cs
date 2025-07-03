using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("teacherworkstatushistory")]
    [Index("Historyid", Name = "teacherworkstatushistory_historyid_key", IsUnique = true)]
    public partial class Teacherworkstatushistory
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("historyid")]
        [StringLength(255)]
        public string Historyid { get; set; } = null!;
        [Column("fk_teacherid")]
        [StringLength(255)]
        public string FkTeacherid { get; set; } = null!;
        [Column("statustype")]
        [StringLength(255)]
        public string Statustype { get; set; } = null!;
        [Column("startdate")]
        public DateOnly Startdate { get; set; }
        [Column("enddate")]
        public DateOnly Enddate { get; set; }
        [Column("note")]
        [StringLength(255)]
        public string? Note { get; set; }
        [Column("decisionfileurl")]
        [StringLength(255)]
        public string? Decisionfileurl { get; set; }
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime Createdat { get; set; }

        public virtual Teacher FkTeacher { get; set; } = null!;
    }
}
