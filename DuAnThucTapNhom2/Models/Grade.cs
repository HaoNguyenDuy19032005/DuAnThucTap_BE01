using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("grades")]
    [Index("Gradeid", Name = "grades_gradeid_key", IsUnique = true)]
    public partial class Grade
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("gradeid")]
        [StringLength(255)]
        public string Gradeid { get; set; } = null!;
        [Column("fk_studentid")]
        [StringLength(255)]
        public string FkStudentid { get; set; } = null!;
        [Column("fk_semesterid")]
        [StringLength(255)]
        public string FkSemesterid { get; set; } = null!;
        [Column("fk_gradetypeid")]
        [StringLength(255)]
        public string FkGradetypeid { get; set; } = null!;
        [Column("score")]
        public double Score { get; set; }
        [Column("instance")]
        public int Instance { get; set; }
        [Column("gradeddate")]
        public DateOnly Gradeddate { get; set; }

        public virtual Gradetype FkGradetype { get; set; } = null!;
        public virtual Semester FkSemester { get; set; } = null!;
        public virtual Student FkStudent { get; set; } = null!;
    }
}
