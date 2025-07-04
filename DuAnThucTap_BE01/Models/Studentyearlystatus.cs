using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("studentyearlystatus")]
    public partial class Studentyearlystatus
    {
        [Key]
        [Column("studentyearlystatusid")]
        public Guid Studentyearlystatusid { get; set; }
        [Column("studentid")]
        public Guid Studentid { get; set; }
        [Column("schoolyearid")]
        public Guid Schoolyearid { get; set; }
        [Column("classid")]
        public Guid? Classid { get; set; }
        [Column("gradelevelid")]
        public Guid Gradelevelid { get; set; }
        [Column("enrollmentstatus")]
        [StringLength(100)]
        public string? Enrollmentstatus { get; set; }
        [Column("enrollmentdate")]
        public DateOnly? Enrollmentdate { get; set; }
        [Column("graduationdate")]
        public DateOnly? Graduationdate { get; set; }
        [Column("notes")]
        public string? Notes { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }
        [Column("updatedat")]
        public DateTime? Updatedat { get; set; }

        [ForeignKey("Classid")]
        [InverseProperty("Studentyearlystatuses")]
        public virtual Class? Class { get; set; }
        [ForeignKey("Gradelevelid")]
        [InverseProperty("Studentyearlystatuses")]
        public virtual Gradelevel Gradelevel { get; set; } = null!;
        [ForeignKey("Schoolyearid")]
        [InverseProperty("Studentyearlystatuses")]
        public virtual Schoolyear Schoolyear { get; set; } = null!;
        [ForeignKey("Studentid")]
        [InverseProperty("Studentyearlystatuses")]
        public virtual Student Student { get; set; } = null!;
    }
}
