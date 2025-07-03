using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("semesters")]
    [Index("Semesterid", Name = "semesters_semesterid_key", IsUnique = true)]
    public partial class Semester
    {
        public Semester()
        {
            Classtransferhistories = new HashSet<Classtransferhistory>();
            Exams = new HashSet<Exam>();
            Grades = new HashSet<Grade>();
            Incomingtransferhistories = new HashSet<Incomingtransferhistory>();
            Studentsemestersummaries = new HashSet<Studentsemestersummary>();
            Studentsubjectsummaries = new HashSet<Studentsubjectsummary>();
            Subjectsofexemptions = new HashSet<Subjectsofexemption>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("semesterid")]
        [StringLength(255)]
        public string Semesterid { get; set; } = null!;
        [Column("semestername")]
        [StringLength(255)]
        public string Semestername { get; set; } = null!;
        [Column("startdate")]
        public DateOnly Startdate { get; set; }
        [Column("enddate")]
        public DateOnly Enddate { get; set; }
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime Createdat { get; set; }
        [Column("updatedat", TypeName = "timestamp without time zone")]
        public DateTime Updatedat { get; set; }
        [Column("fk_teacherid")]
        [StringLength(255)]
        public string FkTeacherid { get; set; } = null!;

        public virtual Teacher FkTeacher { get; set; } = null!;
        public virtual ICollection<Classtransferhistory> Classtransferhistories { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<Incomingtransferhistory> Incomingtransferhistories { get; set; }
        public virtual ICollection<Studentsemestersummary> Studentsemestersummaries { get; set; }
        public virtual ICollection<Studentsubjectsummary> Studentsubjectsummaries { get; set; }
        public virtual ICollection<Subjectsofexemption> Subjectsofexemptions { get; set; }
    }
}
