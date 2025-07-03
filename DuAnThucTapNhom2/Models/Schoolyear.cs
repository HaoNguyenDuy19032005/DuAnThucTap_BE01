using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("schoolyears")]
    [Index("Schoolyearid", Name = "schoolyears_schoolyearid_key", IsUnique = true)]
    public partial class Schoolyear
    {
        public Schoolyear()
        {
            Classes = new HashSet<Class>();
            Classtypes = new HashSet<Classtype>();
            Departmentleaders = new HashSet<Departmentleader>();
            Exams = new HashSet<Exam>();
            Students = new HashSet<Student>();
            Studentsemestersummaries = new HashSet<Studentsemestersummary>();
            Subjects = new HashSet<Subject>();
            Teacherconcurrentsubjects = new HashSet<Teacherconcurrentsubject>();
            Teachers = new HashSet<Teacher>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("schoolyearid")]
        [StringLength(255)]
        public string Schoolyearid { get; set; } = null!;
        [Column("schoolyearname")]
        [StringLength(255)]
        public string Schoolyearname { get; set; } = null!;
        [Column("startyear")]
        public int Startyear { get; set; }
        [Column("endyear")]
        public int Endyear { get; set; }
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime Createdat { get; set; }
        [Column("updatedat", TypeName = "timestamp without time zone")]
        public DateTime Updatedat { get; set; }
        [Column("fk_schoolinfoid")]
        [StringLength(255)]
        public string FkSchoolinfoid { get; set; } = null!;

        public virtual Schoolinformation FkSchoolinfo { get; set; } = null!;
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Classtype> Classtypes { get; set; }
        public virtual ICollection<Departmentleader> Departmentleaders { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Studentsemestersummary> Studentsemestersummaries { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual ICollection<Teacherconcurrentsubject> Teacherconcurrentsubjects { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
