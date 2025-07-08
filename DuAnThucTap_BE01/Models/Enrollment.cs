using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("enrollments")]
    [Index("Studentid", "Classid", Name = "enrollments_studentid_classid_key", IsUnique = true)]
    public partial class Enrollment
    {
        [Key]
        [Column("enrollmentid")]
        public int Enrollmentid { get; set; }
        [Column("studentid")]
        public int Studentid { get; set; }
        [Column("classid")]
        public int Classid { get; set; }
        [Column("enrollmentdate")]
        public DateOnly? Enrollmentdate { get; set; }

        [ForeignKey("Classid")]
        [InverseProperty("Enrollments")]
        public virtual Class Class { get; set; } = null!;
        [ForeignKey("Studentid")]
        [InverseProperty("Enrollments")]
        public virtual Student Student { get; set; } = null!;
    }
}
