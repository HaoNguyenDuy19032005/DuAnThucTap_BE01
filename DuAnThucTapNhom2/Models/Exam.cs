using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("exams")]
    [Index("Examid", Name = "exams_examid_key", IsUnique = true)]
    public partial class Exam
    {
        public Exam()
        {
            Examschedules = new HashSet<Examschedule>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("examid")]
        [StringLength(255)]
        public string Examid { get; set; } = null!;
        [Column("fk_schoolyearid")]
        [StringLength(255)]
        public string FkSchoolyearid { get; set; } = null!;
        [Column("fk_gradelevelid")]
        [StringLength(255)]
        public string FkGradelevelid { get; set; } = null!;
        [Column("fk_semesterid")]
        [StringLength(255)]
        public string FkSemesterid { get; set; } = null!;
        [Column("fk_subjectid")]
        [StringLength(255)]
        public string FkSubjectid { get; set; } = null!;
        [Column("examname")]
        [StringLength(255)]
        public string Examname { get; set; } = null!;
        [Column("examdate")]
        public DateOnly Examdate { get; set; }
        [Column("durationminutes")]
        public int Durationminutes { get; set; }
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime Createdat { get; set; }

        public virtual Gradelevel FkGradelevel { get; set; } = null!;
        public virtual Schoolyear FkSchoolyear { get; set; } = null!;
        public virtual Semester FkSemester { get; set; } = null!;
        public virtual Subject FkSubject { get; set; } = null!;
        public virtual ICollection<Examschedule> Examschedules { get; set; }
    }
}
