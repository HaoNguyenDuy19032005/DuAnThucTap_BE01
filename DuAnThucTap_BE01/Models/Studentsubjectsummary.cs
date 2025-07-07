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
        public Guid Subjectsummaryid { get; set; }
        [Column("studentid")]
        public Guid Studentid { get; set; }
        [Column("subjectid")]
        public Guid Subjectid { get; set; }
        [Column("semesterid")]
        public Guid Semesterid { get; set; }
        [Column("schoolyearid")]
        public Guid Schoolyearid { get; set; }
        [Column("schoolid")]
        public Guid Schoolid { get; set; }
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
