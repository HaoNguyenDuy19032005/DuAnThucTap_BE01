using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("classes")]
    public partial class Class
    {
        public Class()
        {
            ClasstransferhistoryFromclasses = new HashSet<Classtransferhistory>();
            ClasstransferhistoryToclasses = new HashSet<Classtransferhistory>();
            Enrollments = new HashSet<Enrollment>();
            Examschedules = new HashSet<Examschedule>();
            Qnathreads = new HashSet<Qnathread>();
            SchooltransferhistoryFromclasses = new HashSet<Schooltransferhistory>();
            SchooltransferhistoryToclasses = new HashSet<Schooltransferhistory>();
            Studentyearlystatuses = new HashSet<Studentyearlystatus>();
            Testassignments = new HashSet<Testassignment>();
        }

        [Key]
        [Column("classid")]
        public int Classid { get; set; }
        [Column("classname")]
        [StringLength(100)]
        public string Classname { get; set; } = null!;
        [Column("maxstudents")]
        public int? Maxstudents { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("schoolyearid")]
        public int Schoolyearid { get; set; }
        [Column("gradelevelid")]
        public int Gradelevelid { get; set; }
        [Column("classtypeid")]
        public int? Classtypeid { get; set; }
        [Column("teacherid")]
        public int? Teacherid { get; set; }
        [Column("subjectid")]
        public int? Subjectid { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }
        [Column("updatedat")]
        public DateTime? Updatedat { get; set; }

        [ForeignKey("Classtypeid")]
        [InverseProperty("Classes")]
        public virtual Classtype? Classtype { get; set; }
        [ForeignKey("Gradelevelid")]
        [InverseProperty("Classes")]
        public virtual Gradelevel Gradelevel { get; set; } = null!;
        [ForeignKey("Schoolyearid")]
        [InverseProperty("Classes")]
        public virtual Schoolyear Schoolyear { get; set; } = null!;
        [ForeignKey("Subjectid")]
        [InverseProperty("Classes")]
        public virtual Subject? Subject { get; set; }
        [ForeignKey("Teacherid")]
        [InverseProperty("Classes")]
        public virtual Teacher? Teacher { get; set; }
        [InverseProperty("Fromclass")]
        public virtual ICollection<Classtransferhistory> ClasstransferhistoryFromclasses { get; set; }
        [InverseProperty("Toclass")]
        public virtual ICollection<Classtransferhistory> ClasstransferhistoryToclasses { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<Examschedule> Examschedules { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<Qnathread> Qnathreads { get; set; }
        [InverseProperty("Fromclass")]
        public virtual ICollection<Schooltransferhistory> SchooltransferhistoryFromclasses { get; set; }
        [InverseProperty("Toclass")]
        public virtual ICollection<Schooltransferhistory> SchooltransferhistoryToclasses { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<Studentyearlystatus> Studentyearlystatuses { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<Testassignment> Testassignments { get; set; }
    }
}
