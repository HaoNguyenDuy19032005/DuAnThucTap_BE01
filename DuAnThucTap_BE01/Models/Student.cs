using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("students")]
    [Index("Email", Name = "students_email_key", IsUnique = true)]
    [Index("Studentcode", Name = "students_studentcode_key", IsUnique = true)]
    public partial class Student
    {
        public Student()
        {
            Classtransferhistories = new HashSet<Classtransferhistory>();
            Contacts = new HashSet<Contact>();
            Enrollments = new HashSet<Enrollment>();
            Grades = new HashSet<Grade>();
            Schooltransferhistories = new HashSet<Schooltransferhistory>();
            Studentcommendations = new HashSet<Studentcommendation>();
            Studentdisciplines = new HashSet<Studentdiscipline>();
            Studentexemptions = new HashSet<Studentexemption>();
            Studentsemestersummaries = new HashSet<Studentsemestersummary>();
            Studentsubjectsummaries = new HashSet<Studentsubjectsummary>();
            Studenttestsubmissions = new HashSet<Studenttestsubmission>();
            Studentyearlystatuses = new HashSet<Studentyearlystatus>();
        }

        [Key]
        [Column("studentid")]
        public Guid Studentid { get; set; }
        [Column("fullname")]
        [StringLength(150)]
        public string Fullname { get; set; } = null!;
        [Column("gender")]
        [StringLength(10)]
        public string? Gender { get; set; }
        [Column("dateofbirth")]
        public DateOnly? Dateofbirth { get; set; }
        [Column("studentcode")]
        [StringLength(50)]
        public string? Studentcode { get; set; }
        [Column("birthplace")]
        public string? Birthplace { get; set; }
        [Column("enrollmentdate")]
        public DateOnly? Enrollmentdate { get; set; }
        [Column("ethnicity")]
        [StringLength(50)]
        public string? Ethnicity { get; set; }
        [Column("admissiontype")]
        [StringLength(100)]
        public string? Admissiontype { get; set; }
        [Column("religion")]
        [StringLength(50)]
        public string? Religion { get; set; }
        [Column("status")]
        [StringLength(100)]
        public string? Status { get; set; }
        [Column("address_provincecity")]
        [StringLength(100)]
        public string? AddressProvincecity { get; set; }
        [Column("address_district")]
        [StringLength(100)]
        public string? AddressDistrict { get; set; }
        [Column("address_ward")]
        [StringLength(100)]
        public string? AddressWard { get; set; }
        [Column("address_street")]
        public string? AddressStreet { get; set; }
        [Column("email")]
        [StringLength(255)]
        public string? Email { get; set; }
        [Column("phonenumber")]
        [StringLength(20)]
        public string? Phonenumber { get; set; }
        [Column("fathername")]
        [StringLength(150)]
        public string? Fathername { get; set; }
        [Column("fatherbirthyear")]
        public int? Fatherbirthyear { get; set; }
        [Column("fatheroccupation")]
        [StringLength(255)]
        public string? Fatheroccupation { get; set; }
        [Column("mothername")]
        [StringLength(150)]
        public string? Mothername { get; set; }
        [Column("motherbirthyear")]
        public int? Motherbirthyear { get; set; }
        [Column("motheroccupation")]
        [StringLength(255)]
        public string? Motheroccupation { get; set; }
        [Column("guardianname")]
        [StringLength(150)]
        public string? Guardianname { get; set; }
        [Column("guardianoccupation")]
        [StringLength(255)]
        public string? Guardianoccupation { get; set; }
        [Column("phonenumberfather")]
        [StringLength(20)]
        public string? Phonenumberfather { get; set; }
        [Column("phonenumbermother")]
        [StringLength(20)]
        public string? Phonenumbermother { get; set; }
        [Column("phonenumberguardian")]
        [StringLength(20)]
        public string? Phonenumberguardian { get; set; }
        [Column("profileimageurl")]
        public string? Profileimageurl { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }
        [Column("updatedat")]
        public DateTime? Updatedat { get; set; }

        [InverseProperty("Student")]
        public virtual User? User { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<Classtransferhistory> Classtransferhistories { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<Contact> Contacts { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<Grade> Grades { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<Schooltransferhistory> Schooltransferhistories { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<Studentcommendation> Studentcommendations { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<Studentdiscipline> Studentdisciplines { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<Studentexemption> Studentexemptions { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<Studentsemestersummary> Studentsemestersummaries { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<Studentsubjectsummary> Studentsubjectsummaries { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<Studenttestsubmission> Studenttestsubmissions { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<Studentyearlystatus> Studentyearlystatuses { get; set; }
    }
}
