using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace DuAnThucTap_BE01.Models
{
    [Table("exams")]
    public partial class Exam
    {
        public Exam()
        {
            Examschedules = new HashSet<Examschedule>();
        }

        [Key]
        [Column("examid")]
        public int Examid { get; set; }

        // Sửa các khóa ngoại từ int thành int? để chúng không còn là required
        [Column("schoolyearid")]
        public int? Schoolyearid { get; set; } // <--- Sửa ở đây

        [Column("gradelevelid")]
        public int? Gradelevelid { get; set; } // <--- Sửa ở đây

        [Column("semesterid")]
        public int? Semesterid { get; set; } // <--- Sửa ở đây

        [Column("subjectid")]
        public int? Subjectid { get; set; } // <--- Sửa ở đây

        [Column("examname")]
        [StringLength(255)]
        public string Examname { get; set; } = null!;
        [Column("examdate")]
        public DateOnly? Examdate { get; set; }
        [Column("durationminutes")]
        public int? Durationminutes { get; set; }
        [Column("classtypeid")]
        public int? Classtypeid { get; set; }
        [Column("graderassignmenttypeid")]
        public int? Graderassignmenttypeid { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }

        [ForeignKey("Classtypeid")]
        [InverseProperty("Exams")]
        [JsonIgnore]
        public virtual Classtype? Classtype { get; set; }

        [ForeignKey("Gradelevelid")]
        [InverseProperty("Exams")]
        [JsonIgnore]
        public virtual Gradelevel? Gradelevel { get; set; }

        [ForeignKey("Graderassignmenttypeid")]
        [InverseProperty("Exams")]
        [JsonIgnore]
        public virtual Graderassignmenttype? Graderassignmenttype { get; set; }

        [ForeignKey("Schoolyearid")]
        [InverseProperty("Exams")]
        [JsonIgnore]
        public virtual Schoolyear? Schoolyear { get; set; }

        [ForeignKey("Semesterid")]
        [InverseProperty("Exams")]
        [JsonIgnore]
        public virtual Semester? Semester { get; set; }

        [ForeignKey("Subjectid")]
        [InverseProperty("Exams")]
        [JsonIgnore]
        public virtual Subject? Subject { get; set; }

        [InverseProperty("Exam")]
        [JsonIgnore]
        public virtual ICollection<Examschedule> Examschedules { get; set; }
    }
}