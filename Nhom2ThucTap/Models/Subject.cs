using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhom2ThucTap.Models
{
    [Table("subjects")]
    [Index("Subjectcode", Name = "subjects_subjectcode_key", IsUnique = true)]
    public partial class Subject
    {
        public Subject()
        {
            Classes = new HashSet<Class>();
            Exams = new HashSet<Exam>();
            Grades = new HashSet<Grade>();
            Studentsubjectsummaries = new HashSet<Studentsubjectsummary>();
            Teacherconcurrentsubjects = new HashSet<Teacherconcurrentsubject>();
            Teachers = new HashSet<Teacher>();

        }

        [Key]
        [Column("subjectid")]
        public int Subjectid { get; set; }
        [Column("subjectname")]
        [StringLength(255)]
        public string Subjectname { get; set; } = null!;
        [Column("subjectcode")]
        [StringLength(50)]
        public string? Subjectcode { get; set; }
        [Column("defaultperiodssem1")]
        public int? Defaultperiodssem1 { get; set; }
        [Column("defaultperiodssem2")]
        public int? Defaultperiodssem2 { get; set; }
        [Column("departmentid")]
        public int? Departmentid { get; set; }
        [Column("subjecttypeid")]
        public int? Subjecttypeid { get; set; }
        [Column("schoolyearid")]
        public int Schoolyearid { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }
        [Column("updatedat")]
        public DateTime? Updatedat { get; set; }

        [ForeignKey("Departmentid")]
        [InverseProperty("Subjects")]
        public virtual Department? Department { get; set; }
        [ForeignKey("Schoolyearid")]
        [InverseProperty("Subjects")]
        public virtual Schoolyear Schoolyear { get; set; } = null!;
        [ForeignKey("Subjecttypeid")]
        [InverseProperty("Subjects")]
        public virtual Subjecttype? Subjecttype { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Class> Classes { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Exam> Exams { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Grade> Grades { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Studentsubjectsummary> Studentsubjectsummaries { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Teacherconcurrentsubject> Teacherconcurrentsubjects { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Teacher> Teachers { get; set; }


    }
}
