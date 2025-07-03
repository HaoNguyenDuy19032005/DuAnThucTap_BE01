using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("subjectsofexemption")]
    public partial class Subjectsofexemption
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("exemptionname")]
        [StringLength(255)]
        public string Exemptionname { get; set; } = null!;
        [Column("fk_studentid")]
        [StringLength(255)]
        public string FkStudentid { get; set; } = null!;
        [Column("fk_subjectid")]
        [StringLength(255)]
        public string FkSubjectid { get; set; } = null!;
        [Column("commendationdate")]
        public DateOnly Commendationdate { get; set; }
        [Column("fk_semesterid")]
        [StringLength(255)]
        public string FkSemesterid { get; set; } = null!;
        [Column("content")]
        public string? Content { get; set; }
        [Column("attachmenturl")]
        [StringLength(255)]
        public string? Attachmenturl { get; set; }
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime Createdat { get; set; }

        public virtual Semester FkSemester { get; set; } = null!;
        public virtual Student FkStudent { get; set; } = null!;
        public virtual Subject FkSubject { get; set; } = null!;
    }
}
