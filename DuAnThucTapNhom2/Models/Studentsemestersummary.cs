using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("studentsemestersummary")]
    [Index("Summaryid", Name = "studentsemestersummary_summaryid_key", IsUnique = true)]
    public partial class Studentsemestersummary
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("summaryid")]
        [StringLength(255)]
        public string Summaryid { get; set; } = null!;
        [Column("fk_studentid")]
        [StringLength(255)]
        public string FkStudentid { get; set; } = null!;
        [Column("fk_semesterid")]
        [StringLength(255)]
        public string FkSemesterid { get; set; } = null!;
        [Column("schoolyearid")]
        [StringLength(255)]
        public string Schoolyearid { get; set; } = null!;
        [Column("averagescore")]
        public double Averagescore { get; set; }
        [Column("performancerating")]
        [StringLength(255)]
        public string Performancerating { get; set; } = null!;
        [Column("conductrating")]
        [StringLength(255)]
        public string Conductrating { get; set; } = null!;

        public virtual Semester FkSemester { get; set; } = null!;
        public virtual Student FkStudent { get; set; } = null!;
        public virtual Schoolyear Schoolyear { get; set; } = null!;
    }
}
