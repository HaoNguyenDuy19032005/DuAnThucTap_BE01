using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("studentsubjectsummary")]
    [Index("Studentsubjectsummaryid", Name = "studentsubjectsummary_studentsubjectsummaryid_key", IsUnique = true)]
    public partial class Studentsubjectsummary
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("studentsubjectsummaryid")]
        [StringLength(255)]
        public string Studentsubjectsummaryid { get; set; } = null!;
        [Column("fk_studentid")]
        [StringLength(255)]
        public string FkStudentid { get; set; } = null!;
        [Column("fk_subjectid")]
        [StringLength(255)]
        public string FkSubjectid { get; set; } = null!;
        [Column("fk_semesterid")]
        [StringLength(255)]
        public string FkSemesterid { get; set; } = null!;
        [Column("averagescore")]
        public double Averagescore { get; set; }
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime Createdat { get; set; }

        public virtual Semester FkSemester { get; set; } = null!;
        public virtual Student FkStudent { get; set; } = null!;
        public virtual Subject FkSubject { get; set; } = null!;
    }
}
