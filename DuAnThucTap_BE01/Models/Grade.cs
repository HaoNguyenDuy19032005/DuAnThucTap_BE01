using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("grades")]
    public partial class Grade
    {
        [Key]
        [Column("gradeid")]
        public Guid Gradeid { get; set; }
        [Column("studentid")]
        public Guid Studentid { get; set; }
        [Column("subjectid")]
        public Guid Subjectid { get; set; }
        [Column("semesterid")]
        public Guid Semesterid { get; set; }
        [Column("gradetypeid")]
        public Guid Gradetypeid { get; set; }
        [Column("schoolid")]
        public Guid Schoolid { get; set; }
        [Column("score")]
        [Precision(4, 2)]
        public decimal Score { get; set; }
        [Column("instance")]
        public int Instance { get; set; }
        [Column("gradeddate")]
        public DateOnly? Gradeddate { get; set; }

        [ForeignKey("Gradetypeid")]
        [InverseProperty("Grades")]
        public virtual Gradetype Gradetype { get; set; } = null!;
        [ForeignKey("Schoolid")]
        [InverseProperty("Grades")]
        public virtual Schoolinformation School { get; set; } = null!;
        [ForeignKey("Semesterid")]
        [InverseProperty("Grades")]
        public virtual Semester Semester { get; set; } = null!;
        [ForeignKey("Studentid")]
        [InverseProperty("Grades")]
        public virtual Student Student { get; set; } = null!;
        [ForeignKey("Subjectid")]
        [InverseProperty("Grades")]
        public virtual Subject Subject { get; set; } = null!;
    }
}
