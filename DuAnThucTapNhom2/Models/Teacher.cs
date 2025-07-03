using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("teachers")]
    [Index("Teacherid", Name = "teachers_teacherid_key", IsUnique = true)]
    public partial class Teacher
    {
        public Teacher()
        {
            Classes = new HashSet<Class>();
            Classtypes = new HashSet<Classtype>();
            Courses = new HashSet<Course>();
            Departmentleaders = new HashSet<Departmentleader>();
            Examgraders = new HashSet<Examgrader>();
            Gradelevels = new HashSet<Gradelevel>();
            Lessons = new HashSet<Lesson>();
            Semesters = new HashSet<Semester>();
            Studenttestsubmissions = new HashSet<Studenttestsubmission>();
            Teacherconcurrentsubjects = new HashSet<Teacherconcurrentsubject>();
            Teachertraininghistories = new HashSet<Teachertraininghistory>();
            Teacherworkhistories = new HashSet<Teacherworkhistory>();
            Teacherworkstatushistories = new HashSet<Teacherworkstatushistory>();
            Teachingassignments = new HashSet<Teachingassignment>();
            Tests = new HashSet<Test>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("teacherid")]
        [StringLength(255)]
        public string Teacherid { get; set; } = null!;
        [Column("teachercode")]
        [StringLength(255)]
        public string Teachercode { get; set; } = null!;
        [Column("fullname")]
        [StringLength(255)]
        public string Fullname { get; set; } = null!;
        [Column("dateofbirth")]
        public DateOnly Dateofbirth { get; set; }
        [Column("gender")]
        [StringLength(255)]
        public string Gender { get; set; } = null!;
        [Column("ethnicity")]
        [StringLength(255)]
        public string Ethnicity { get; set; } = null!;
        [Column("hiredate")]
        public DateOnly Hiredate { get; set; }
        [Column("nationality")]
        [StringLength(255)]
        public string Nationality { get; set; } = null!;
        [Column("religion")]
        [StringLength(255)]
        public string Religion { get; set; } = null!;
        [Column("status")]
        [StringLength(255)]
        public string Status { get; set; } = null!;
        [Column("alias")]
        [StringLength(255)]
        public string Alias { get; set; } = null!;
        [Column("address_provincecity")]
        [StringLength(255)]
        public string AddressProvincecity { get; set; } = null!;
        [Column("address_ward")]
        [StringLength(255)]
        public string AddressWard { get; set; } = null!;
        [Column("address_district")]
        [StringLength(255)]
        public string AddressDistrict { get; set; } = null!;
        [Column("address_street")]
        [StringLength(255)]
        public string AddressStreet { get; set; } = null!;
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; } = null!;
        [Column("phonenumber")]
        [StringLength(255)]
        public string Phonenumber { get; set; } = null!;
        [Column("dateofjoiningtheparty")]
        public DateOnly? Dateofjoiningtheparty { get; set; }
        [Column("dateofjoininggroup")]
        public DateOnly? Dateofjoininggroup { get; set; }
        [Column("ispartymember")]
        public bool Ispartymember { get; set; }
        [Column("fk_departmentid")]
        [StringLength(255)]
        public string FkDepartmentid { get; set; } = null!;
        [Column("fk_subjectid")]
        [StringLength(255)]
        public string? FkSubjectid { get; set; }
        [Column("fk_schoolyearid")]
        [StringLength(255)]
        public string FkSchoolyearid { get; set; } = null!;
        [Column("fk_contactid")]
        [StringLength(255)]
        public string FkContactid { get; set; } = null!;

        public virtual Contact FkContact { get; set; } = null!;
        public virtual Department FkDepartment { get; set; } = null!;
        public virtual Schoolyear FkSchoolyear { get; set; } = null!;
        public virtual Subject? FkSubject { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Classtype> Classtypes { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Departmentleader> Departmentleaders { get; set; }
        public virtual ICollection<Examgrader> Examgraders { get; set; }
        public virtual ICollection<Gradelevel> Gradelevels { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Semester> Semesters { get; set; }
        public virtual ICollection<Studenttestsubmission> Studenttestsubmissions { get; set; }
        public virtual ICollection<Teacherconcurrentsubject> Teacherconcurrentsubjects { get; set; }
        public virtual ICollection<Teachertraininghistory> Teachertraininghistories { get; set; }
        public virtual ICollection<Teacherworkhistory> Teacherworkhistories { get; set; }
        public virtual ICollection<Teacherworkstatushistory> Teacherworkstatushistories { get; set; }
        public virtual ICollection<Teachingassignment> Teachingassignments { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}
