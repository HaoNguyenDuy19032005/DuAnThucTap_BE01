using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhom2ThucTap.Models
{
    [Table("grades")]
    public partial class Grade
    {
        [Key]
        [Column("gradeid")]
        public int Gradeid { get; set; }
        [Column("studentid")]
        public int Studentid { get; set; }
        [Column("subjectid")]
        public int Subjectid { get; set; }
        [Column("semesterid")]
        public int Semesterid { get; set; }
        [Column("gradetypeid")]
        public int Gradetypeid { get; set; }
        [Column("schoolinfoid")]
        public int Schoolinfoid { get; set; }
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
        [ForeignKey("Schoolinfoid")]
        [InverseProperty("Grades")]
        public virtual Schoolinformation Schoolinfo { get; set; } = null!;
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
