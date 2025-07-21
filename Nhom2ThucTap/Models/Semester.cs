using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhom2ThucTap.Models
{
    [Table("semesters")]
    public partial class Semester
    {
        public Semester()
        {
            Classtransferhistories = new HashSet<Classtransferhistory>();
            Exams = new HashSet<Exam>();
            Grades = new HashSet<Grade>();
            Schooltransferhistories = new HashSet<Schooltransferhistory>();
            Studentcommendations = new HashSet<Studentcommendation>();
            Studentdisciplines = new HashSet<Studentdiscipline>();
            Studentpreservations = new HashSet<Studentpreservation>();
            Studentsemestersummaries = new HashSet<Studentsemestersummary>();
            Studentsubjectsummaries = new HashSet<Studentsubjectsummary>();
            StudentTransferReceipts = new HashSet<StudentTransferReceipt>();
        }

        [Key]
        [Column("semesterid")]
        public int Semesterid { get; set; }
        [Column("schoolyearid")]
        public int Schoolyearid { get; set; }
        [Column("semestername")]
        [StringLength(100)]
        public string Semestername { get; set; } = null!;
        [Column("startdate")]
        public DateOnly Startdate { get; set; }
        [Column("enddate")]
        public DateOnly Enddate { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }
        [Column("updatedat")]
        public DateTime? Updatedat { get; set; }

        [ForeignKey("Schoolyearid")]
        [InverseProperty("Semesters")]
        public virtual Schoolyear Schoolyear { get; set; } = null!;
        [InverseProperty("Semester")]
        public virtual ICollection<Classtransferhistory> Classtransferhistories { get; set; }
        [InverseProperty("Semester")]
        public virtual ICollection<Exam> Exams { get; set; }
        [InverseProperty("Semester")]
        public virtual ICollection<Grade> Grades { get; set; }
        [InverseProperty("Semester")]
        public virtual ICollection<Schooltransferhistory> Schooltransferhistories { get; set; }
        [InverseProperty("Semester")]
        public virtual ICollection<Studentcommendation> Studentcommendations { get; set; }
        [InverseProperty("Semester")]
        public virtual ICollection<Studentdiscipline> Studentdisciplines { get; set; }
        [InverseProperty("Semester")]
        public virtual ICollection<Studentpreservation> Studentpreservations { get; set; }
        [InverseProperty("Semester")]
        public virtual ICollection<Studentsemestersummary> Studentsemestersummaries { get; set; }
        [InverseProperty("Semester")]
        public virtual ICollection<Studentsubjectsummary> Studentsubjectsummaries { get; set; }
        [InverseProperty("Semester")]
        public virtual ICollection<StudentTransferReceipt> StudentTransferReceipts { get; set; } = new List<StudentTransferReceipt>();

    }
}
