using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("subjects")]
    [Index("Subjectid", Name = "subjects_subjectid_key", IsUnique = true)]
    public partial class Subject
    {
        public Subject()
        {
            Classtypes = new HashSet<Classtype>();
            Exams = new HashSet<Exam>();
            Studentsubjectsummaries = new HashSet<Studentsubjectsummary>();
            Subjectsofexemptions = new HashSet<Subjectsofexemption>();
            Teacherconcurrentsubjects = new HashSet<Teacherconcurrentsubject>();
            Teachers = new HashSet<Teacher>();
            Teachingassignments = new HashSet<Teachingassignment>();
            Topiclists = new HashSet<Topiclist>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("subjectid")]
        [StringLength(255)]
        public string Subjectid { get; set; } = null!;
        [Column("subjectname")]
        [StringLength(255)]
        public string Subjectname { get; set; } = null!;
        [Column("subjectcode")]
        [StringLength(255)]
        public string Subjectcode { get; set; } = null!;
        [Column("defaultperiodssem1")]
        public int Defaultperiodssem1 { get; set; }
        [Column("defaultperiodssem2")]
        public int Defaultperiodssem2 { get; set; }
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime Createdat { get; set; }
        [Column("updatedat", TypeName = "timestamp without time zone")]
        public DateTime Updatedat { get; set; }
        [Column("fk_departmentid")]
        [StringLength(255)]
        public string FkDepartmentid { get; set; } = null!;
        [Column("fk_subjecttypeid")]
        [StringLength(255)]
        public string FkSubjecttypeid { get; set; } = null!;
        [Column("fk_schoolyearid")]
        [StringLength(255)]
        public string FkSchoolyearid { get; set; } = null!;

        public virtual Department FkDepartment { get; set; } = null!;
        public virtual Schoolyear FkSchoolyear { get; set; } = null!;
        public virtual Subjecttype FkSubjecttype { get; set; } = null!;
        public virtual ICollection<Classtype> Classtypes { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<Studentsubjectsummary> Studentsubjectsummaries { get; set; }
        public virtual ICollection<Subjectsofexemption> Subjectsofexemptions { get; set; }
        public virtual ICollection<Teacherconcurrentsubject> Teacherconcurrentsubjects { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Teachingassignment> Teachingassignments { get; set; }
        public virtual ICollection<Topiclist> Topiclists { get; set; }
    }
}
