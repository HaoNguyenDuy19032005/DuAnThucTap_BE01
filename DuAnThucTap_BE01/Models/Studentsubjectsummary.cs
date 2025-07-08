using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("studentsubjectsummary")]
    [Index("Studentid", "Subjectid", "Semesterid", Name = "studentsubjectsummary_studentid_subjectid_semesterid_key", IsUnique = true)]
    public partial class Studentsubjectsummary
    {
        [Key]
        [Column("subjectsummaryid")]
        public int Subjectsummaryid { get; set; }
        [Column("studentid")]
        public int Studentid { get; set; }
        [Column("subjectid")]
        public int Subjectid { get; set; }
        [Column("semesterid")]
        public int Semesterid { get; set; }
        [Column("schoolyearid")]
        public int Schoolyearid { get; set; }
        [Column("schoolid")]
        public int Schoolid { get; set; }
        [Column("averagescore")]
        [Precision(4, 2)]
        public decimal? Averagescore { get; set; }

        [ForeignKey("Schoolid")]
        [InverseProperty("Studentsubjectsummaries")]
        public virtual Schoolinformation School { get; set; } = null!;
        [ForeignKey("Schoolyearid")]
        [InverseProperty("Studentsubjectsummaries")]
        public virtual Schoolyear Schoolyear { get; set; } = null!;
        [ForeignKey("Semesterid")]
        [InverseProperty("Studentsubjectsummaries")]
        public virtual Semester Semester { get; set; } = null!;
        [ForeignKey("Studentid")]
        [InverseProperty("Studentsubjectsummaries")]
        public virtual Student Student { get; set; } = null!;
        [ForeignKey("Subjectid")]
        [InverseProperty("Studentsubjectsummaries")]
        public virtual Subject Subject { get; set; } = null!;
    }
}
