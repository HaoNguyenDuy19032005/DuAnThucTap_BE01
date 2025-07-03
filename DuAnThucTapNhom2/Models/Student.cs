using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("students")]
    [Index("Studentid", Name = "students_studentid_key", IsUnique = true)]
    public partial class Student
    {
        public Student()
        {
            Classtransferhistories = new HashSet<Classtransferhistory>();
            Enrollments = new HashSet<Enrollment>();
            Files = new HashSet<File>();
            Grades = new HashSet<Grade>();
            Incomingtransferhistories = new HashSet<Incomingtransferhistory>();
            Schooltransferhistories = new HashSet<Schooltransferhistory>();
            Studentcommendations = new HashSet<Studentcommendation>();
            Studentdisciplines = new HashSet<Studentdiscipline>();
            Studentexemptions = new HashSet<Studentexemption>();
            Studentsemestersummaries = new HashSet<Studentsemestersummary>();
            Studentsubjectsummaries = new HashSet<Studentsubjectsummary>();
            Studenttestsubmissions = new HashSet<Studenttestsubmission>();
            Subjectsofexemptions = new HashSet<Subjectsofexemption>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("studentid")]
        [StringLength(255)]
        public string Studentid { get; set; } = null!;
        [Column("fullname")]
        [StringLength(255)]
        public string Fullname { get; set; } = null!;
        [Column("gender")]
        [StringLength(255)]
        public string Gender { get; set; } = null!;
        [Column("dateofbirth")]
        public DateOnly Dateofbirth { get; set; }
        [Column("studentcode")]
        [StringLength(255)]
        public string Studentcode { get; set; } = null!;
        [Column("birthplace")]
        [StringLength(255)]
        public string Birthplace { get; set; } = null!;
        [Column("enrollmentdate")]
        public DateOnly Enrollmentdate { get; set; }
        [Column("ethnicity")]
        [StringLength(255)]
        public string Ethnicity { get; set; } = null!;
        [Column("admissiontype")]
        [StringLength(255)]
        public string Admissiontype { get; set; } = null!;
        [Column("religion")]
        [StringLength(255)]
        public string Religion { get; set; } = null!;
        [Column("status")]
        [StringLength(255)]
        public string Status { get; set; } = null!;
        [Column("address_provincecity")]
        [StringLength(255)]
        public string AddressProvincecity { get; set; } = null!;
        [Column("address_district")]
        [StringLength(255)]
        public string AddressDistrict { get; set; } = null!;
        [Column("address_ward")]
        [StringLength(255)]
        public string AddressWard { get; set; } = null!;
        [Column("address_street")]
        [StringLength(255)]
        public string AddressStreet { get; set; } = null!;
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; } = null!;
        [Column("phonenumber")]
        [StringLength(255)]
        public string Phonenumber { get; set; } = null!;
        [Column("fathername")]
        [StringLength(255)]
        public string Fathername { get; set; } = null!;
        [Column("fatherbirthyear")]
        public int Fatherbirthyear { get; set; }
        [Column("fatheroccupation")]
        [StringLength(255)]
        public string Fatheroccupation { get; set; } = null!;
        [Column("mothername")]
        [StringLength(255)]
        public string Mothername { get; set; } = null!;
        [Column("motherbirthyear")]
        public int Motherbirthyear { get; set; }
        [Column("motheroccupation")]
        [StringLength(255)]
        public string Motheroccupation { get; set; } = null!;
        [Column("guardianname")]
        [StringLength(255)]
        public string Guardianname { get; set; } = null!;
        [Column("guardianbirthyear")]
        public int Guardianbirthyear { get; set; }
        [Column("guardianoccupation")]
        [StringLength(255)]
        public string Guardianoccupation { get; set; } = null!;
        [Column("phonenumberfather")]
        [StringLength(255)]
        public string Phonenumberfather { get; set; } = null!;
        [Column("phonenumbermother")]
        [StringLength(255)]
        public string Phonenumbermother { get; set; } = null!;
        [Column("phonenumberguardian")]
        [StringLength(255)]
        public string Phonenumberguardian { get; set; } = null!;
        [Column("profileimageurl")]
        [StringLength(255)]
        public string? Profileimageurl { get; set; }
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime Createdat { get; set; }
        [Column("updatedat", TypeName = "timestamp without time zone")]
        public DateTime Updatedat { get; set; }
        [Column("fk_schoolyearid")]
        [StringLength(255)]
        public string FkSchoolyearid { get; set; } = null!;
        [Column("gradelevelid")]
        [StringLength(255)]
        public string Gradelevelid { get; set; } = null!;
        [Column("fk_classid")]
        [StringLength(255)]
        public string FkClassid { get; set; } = null!;

        // Sửa các navigation property thành nullable để tránh lỗi bắt buộc truyền khi tạo mới
        public virtual Class? FkClass { get; set; }
        public virtual Schoolyear? FkSchoolyear { get; set; }
        public virtual Gradelevel? Gradelevel { get; set; }
        public virtual ICollection<Classtransferhistory> Classtransferhistories { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<Incomingtransferhistory> Incomingtransferhistories { get; set; }
        public virtual ICollection<Schooltransferhistory> Schooltransferhistories { get; set; }
        public virtual ICollection<Studentcommendation> Studentcommendations { get; set; }
        public virtual ICollection<Studentdiscipline> Studentdisciplines { get; set; }
        public virtual ICollection<Studentexemption> Studentexemptions { get; set; }
        public virtual ICollection<Studentsemestersummary> Studentsemestersummaries { get; set; }
        public virtual ICollection<Studentsubjectsummary> Studentsubjectsummaries { get; set; }
        public virtual ICollection<Studenttestsubmission> Studenttestsubmissions { get; set; }
        public virtual ICollection<Subjectsofexemption> Subjectsofexemptions { get; set; }
    }
}
