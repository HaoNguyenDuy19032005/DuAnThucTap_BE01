using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("exams")]
    public partial class Exam
    {
        public Exam()
        {
            Examschedules = new HashSet<Examschedule>();
        }

        [Key]
        [Column("examid")]
        public Guid Examid { get; set; }
        [Column("schoolyearid")]
        public Guid Schoolyearid { get; set; }
        [Column("gradelevelid")]
        public Guid Gradelevelid { get; set; }
        [Column("semesterid")]
        public Guid Semesterid { get; set; }
        [Column("subjectid")]
        public Guid Subjectid { get; set; }
        [Column("examname")]
        [StringLength(255)]
        public string Examname { get; set; } = null!;
        [Column("examdate")]
        public DateOnly? Examdate { get; set; }
        [Column("durationminutes")]
        public int? Durationminutes { get; set; }
        [Column("classtypeid")]
        public Guid? Classtypeid { get; set; }
        [Column("graderassignmenttypeid")]
        public Guid? Graderassignmenttypeid { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }

        [ForeignKey("Classtypeid")]
        [InverseProperty("Exams")]
        public virtual Classtype? Classtype { get; set; }
        [ForeignKey("Gradelevelid")]
        [InverseProperty("Exams")]
        public virtual Gradelevel Gradelevel { get; set; } = null!;
        [ForeignKey("Graderassignmenttypeid")]
        [InverseProperty("Exams")]
        public virtual Graderassignmenttype? Graderassignmenttype { get; set; }
        [ForeignKey("Schoolyearid")]
        [InverseProperty("Exams")]
        public virtual Schoolyear Schoolyear { get; set; } = null!;
        [ForeignKey("Semesterid")]
        [InverseProperty("Exams")]
        public virtual Semester Semester { get; set; } = null!;
        [ForeignKey("Subjectid")]
        [InverseProperty("Exams")]
        public virtual Subject Subject { get; set; } = null!;
        [InverseProperty("Exam")]
        public virtual ICollection<Examschedule> Examschedules { get; set; }
    }
}
