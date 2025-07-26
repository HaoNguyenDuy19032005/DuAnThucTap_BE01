using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("schoolyears")]
    public partial class Schoolyear
    {
        public Schoolyear()
        {
            Blockleaders = new HashSet<Blockleader>();
            Classes = new HashSet<Class>();
            Departmentleaders = new HashSet<Departmentleader>();
            Exams = new HashSet<Exam>();
            Semesters = new HashSet<Semester>();
            Studentsemestersummaries = new HashSet<Studentsemestersummary>();
            Studentsubjectsummaries = new HashSet<Studentsubjectsummary>();
            Studentyearlystatuses = new HashSet<Studentyearlystatus>();
            Subjects = new HashSet<Subject>();
            Teacherconcurrentsubjects = new HashSet<Teacherconcurrentsubject>();
            Teachers = new HashSet<Teacher>();
            Teachingassignments = new HashSet<Teachingassignment>();
        }

        [Key]
        [Column("schoolyearid")]
        public int Schoolyearid { get; set; }
        [Column("schoolinfoid")]
        public int Schoolinfoid { get; set; }
        [Column("schoolyearname")]
        [StringLength(100)]
        public string Schoolyearname { get; set; } = null!;
        [Column("startyear")]
        public int Startyear { get; set; }
        [Column("endyear")]
        public int Endyear { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }
        [Column("updatedat")]
        public DateTime? Updatedat { get; set; }

        [ForeignKey("Schoolinfoid")]
        [InverseProperty("Schoolyears")]
        public virtual Schoolinformation Schoolinfo { get; set; } = null!;
        [InverseProperty("Schoolyear")]
        public virtual ICollection<Blockleader> Blockleaders { get; set; }
        [InverseProperty("Schoolyear")]
        public virtual ICollection<Class> Classes { get; set; }
        [InverseProperty("Schoolyear")]
        public virtual ICollection<Departmentleader> Departmentleaders { get; set; }
        [InverseProperty("Schoolyear")]
        public virtual ICollection<Exam> Exams { get; set; }
        [InverseProperty("Schoolyear")]
        public virtual ICollection<Semester> Semesters { get; set; }
        [InverseProperty("Schoolyear")]
        public virtual ICollection<Studentsemestersummary> Studentsemestersummaries { get; set; }
        [InverseProperty("Schoolyear")]
        public virtual ICollection<Studentsubjectsummary> Studentsubjectsummaries { get; set; }
        [InverseProperty("Schoolyear")]
        public virtual ICollection<Studentyearlystatus> Studentyearlystatuses { get; set; }
        [InverseProperty("Schoolyear")]
        public virtual ICollection<Subject> Subjects { get; set; }
        [InverseProperty("Schoolyear")]
        public virtual ICollection<Teacherconcurrentsubject> Teacherconcurrentsubjects { get; set; }
        [InverseProperty("Schoolyear")]
        public virtual ICollection<Teacher> Teachers { get; set; }
        [InverseProperty("Schoolyear")]
        public virtual ICollection<Teachingassignment> Teachingassignments { get; set; }
    }
}
