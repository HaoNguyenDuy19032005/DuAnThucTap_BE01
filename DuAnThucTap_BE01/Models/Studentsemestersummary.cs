using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("studentsemestersummary")]
    [Index("Studentid", "Semesterid", Name = "studentsemestersummary_studentid_semesterid_key", IsUnique = true)]
    public partial class Studentsemestersummary
    {
        [Key]
        [Column("summaryid")]
        public int Summaryid { get; set; }
        [Column("studentid")]
        public int Studentid { get; set; }
        [Column("semesterid")]
        public int Semesterid { get; set; }
        [Column("schoolyearid")]
        public int Schoolyearid { get; set; }
        [Column("schoolid")]
        public int Schoolid { get; set; }
        [Column("averagescore")]
        [Precision(4, 2)]
        public decimal? Averagescore { get; set; }
        [Column("performancerating")]
        [StringLength(100)]
        public string? Performancerating { get; set; }
        [Column("conductrating")]
        [StringLength(100)]
        public string? Conductrating { get; set; }
        [Column("calculateddate")]
        public DateOnly? Calculateddate { get; set; }

        [ForeignKey("Schoolid")]
        [InverseProperty("Studentsemestersummaries")]
        public virtual Schoolinformation School { get; set; } = null!;
        [ForeignKey("Schoolyearid")]
        [InverseProperty("Studentsemestersummaries")]
        public virtual Schoolyear Schoolyear { get; set; } = null!;
        [ForeignKey("Semesterid")]
        [InverseProperty("Studentsemestersummaries")]
        public virtual Semester Semester { get; set; } = null!;
        [ForeignKey("Studentid")]
        [InverseProperty("Studentsemestersummaries")]
        public virtual Student Student { get; set; } = null!;
    }
}
