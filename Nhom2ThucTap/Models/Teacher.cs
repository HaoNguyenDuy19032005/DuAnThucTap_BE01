using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhom2ThucTap.Models
{
    [Table("teachers")]
    [Index("Email", Name = "teachers_email_key", IsUnique = true)]
    [Index("Teachercode", Name = "teachers_teachercode_key", IsUnique = true)]
    public partial class Teacher
    {
        public Teacher()
        {
            Classes = new HashSet<Class>();
            Contacts = new HashSet<Contact>();
            Courses = new HashSet<Course>();
            Departmentleaders = new HashSet<Departmentleader>();
            Examgraders = new HashSet<Examgrader>();
            Gradelevels = new HashSet<Gradelevel>();
            Lessons = new HashSet<Lesson>();
            Teacherconcurrentsubjects = new HashSet<Teacherconcurrentsubject>();
            Teachertraininghistories = new HashSet<Teachertraininghistory>();
            Teacherworkhistories = new HashSet<Teacherworkhistory>();
            Teacherworkstatushistories = new HashSet<Teacherworkstatushistory>();
            Tests = new HashSet<Test>();
        }

        [Key]
        [Column("teacherid")]
        public int Teacherid { get; set; }
        [Column("teachercode")]
        [StringLength(50)]
        public string? Teachercode { get; set; }
        [Column("fullname")]
        [StringLength(150)]
        public string Fullname { get; set; } = null!;
        [Column("dateofbirth")]
        public DateOnly? Dateofbirth { get; set; }
        [Column("gender")]
        [StringLength(10)]
        public string? Gender { get; set; }
        [Column("ethnicity")]
        [StringLength(50)]
        public string? Ethnicity { get; set; }
        [Column("hiredate")]
        public DateOnly? Hiredate { get; set; }
        [Column("nationality")]
        [StringLength(100)]
        public string? Nationality { get; set; }
        [Column("religion")]
        [StringLength(50)]
        public string? Religion { get; set; }
        [Column("status")]
        [StringLength(100)]
        public string? Status { get; set; }
        [Column("alias")]
        [StringLength(150)]
        public string? Alias { get; set; }
        [Column("address_provincecity")]
        [StringLength(100)]
        public string? AddressProvincecity { get; set; }
        [Column("address_ward")]
        [StringLength(100)]
        public string? AddressWard { get; set; }
        [Column("address_district")]
        [StringLength(100)]
        public string? AddressDistrict { get; set; }
        [Column("address_street")]
        public string? AddressStreet { get; set; }
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; } = null!;
        [Column("phonenumber")]
        [StringLength(20)]
        public string? Phonenumber { get; set; }
        [Column("dateofjoiningtheparty")]
        public DateOnly? Dateofjoiningtheparty { get; set; }
        [Column("avatarurl")]
        [StringLength(255)]
        public string? Avatarurl { get; set; }
        [Column("dateofjoininggroup")]
        public DateOnly? Dateofjoininggroup { get; set; }
        [Column("ispartymember")]
        public bool? Ispartymember { get; set; }
        [Column("departmentid")]
        public int? Departmentid { get; set; }
        [Column("subjectid")]
        public int? Subjectid { get; set; }
        [Column("schoolyearid")]
        public int? Schoolyearid { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }
        [Column("updatedat")]
        public DateTime? Updatedat { get; set; }

        [ForeignKey("Departmentid")]
        [InverseProperty("Teachers")]
        public virtual Department? Department { get; set; }
        [ForeignKey("Schoolyearid")]
        [InverseProperty("Teachers")]
        public virtual Schoolyear? Schoolyear { get; set; }
        [ForeignKey("Subjectid")]
        [InverseProperty("Teachers")]
        public virtual Subject? Subject { get; set; }
        [InverseProperty("Teacher")]
        public virtual User? User { get; set; }
        [InverseProperty("Teacher")]
        public virtual ICollection<Class> Classes { get; set; }
        [InverseProperty("Teacher")]
        public virtual ICollection<Contact> Contacts { get; set; }
        [InverseProperty("Teacher")]
        public virtual ICollection<Course> Courses { get; set; }
        [InverseProperty("Teacher")]
        public virtual ICollection<Departmentleader> Departmentleaders { get; set; }
        [InverseProperty("Teacher")]
        public virtual ICollection<Examgrader> Examgraders { get; set; }
        [InverseProperty("Teacher")]
        public virtual ICollection<Gradelevel> Gradelevels { get; set; }
        [InverseProperty("Teacher")]
        public virtual ICollection<Lesson> Lessons { get; set; }
        [InverseProperty("Teacher")]
        public virtual ICollection<Teacherconcurrentsubject> Teacherconcurrentsubjects { get; set; }
        [InverseProperty("Teacher")]
        public virtual ICollection<Teachertraininghistory> Teachertraininghistories { get; set; }
        [InverseProperty("Teacher")]
        public virtual ICollection<Teacherworkhistory> Teacherworkhistories { get; set; }
        [InverseProperty("Teacher")]
        public virtual ICollection<Teacherworkstatushistory> Teacherworkstatushistories { get; set; }
        [InverseProperty("Teacher")]
        public virtual ICollection<Test> Tests { get; set; }
    }
}
