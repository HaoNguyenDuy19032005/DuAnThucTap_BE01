using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("examschedules")]
    [Index("Examscheduleid", Name = "examschedules_examscheduleid_key", IsUnique = true)]
    public partial class Examschedule
    {
        public Examschedule()
        {
            Examgraders = new HashSet<Examgrader>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("examscheduleid")]
        [StringLength(255)]
        public string Examscheduleid { get; set; } = null!;
        [Column("fk_examid")]
        [StringLength(255)]
        public string FkExamid { get; set; } = null!;
        [Column("fk_classid")]
        [StringLength(255)]
        public string FkClassid { get; set; } = null!;

        public virtual Class FkClass { get; set; } = null!;
        public virtual Exam FkExam { get; set; } = null!;
        public virtual ICollection<Examgrader> Examgraders { get; set; }
    }
}
