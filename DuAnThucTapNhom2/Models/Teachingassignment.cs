using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("teachingassignments")]
    [Index("Assignmentid", Name = "teachingassignments_assignmentid_key", IsUnique = true)]
    public partial class Teachingassignment
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("assignmentid")]
        [StringLength(255)]
        public string Assignmentid { get; set; } = null!;
        [Column("fk_teacherid")]
        [StringLength(255)]
        public string FkTeacherid { get; set; } = null!;
        [Column("fk_subjectid")]
        [StringLength(255)]
        public string FkSubjectid { get; set; } = null!;
        [Column("fk_classtypeid")]
        [StringLength(255)]
        public string FkClasstypeid { get; set; } = null!;
        [Column("teachingstartdate")]
        public DateOnly Teachingstartdate { get; set; }
        [Column("teachingenddate")]
        public DateOnly Teachingenddate { get; set; }
        [Column("notes")]
        [StringLength(255)]
        public string? Notes { get; set; }

        public virtual Classtype FkClasstype { get; set; } = null!;
        public virtual Subject FkSubject { get; set; } = null!;
        public virtual Teacher FkTeacher { get; set; } = null!;
    }
}
