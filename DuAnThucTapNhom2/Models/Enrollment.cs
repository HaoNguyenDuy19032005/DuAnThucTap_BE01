using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("enrollments")]
    [Index("Enrollmentid", Name = "enrollments_enrollmentid_key", IsUnique = true)]
    public partial class Enrollment
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("enrollmentid")]
        [StringLength(255)]
        public string Enrollmentid { get; set; } = null!;
        [Column("fk_studentid")]
        [StringLength(255)]
        public string FkStudentid { get; set; } = null!;
        [Column("fk_classid")]
        [StringLength(255)]
        public string FkClassid { get; set; } = null!;
        [Column("body")]
        public string Body { get; set; } = null!;
        [Column("enrollmentdate", TypeName = "timestamp without time zone")]
        public DateTime Enrollmentdate { get; set; }
        [Column("durationinminutes")]
        public int Durationinminutes { get; set; }
        [Column("starttime", TypeName = "timestamp without time zone")]
        public DateTime Starttime { get; set; }
        [Column("endtime", TypeName = "timestamp without time zone")]
        public DateTime Endtime { get; set; }
        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; } = null!;
        [Column("classification")]
        [StringLength(255)]
        public string Classification { get; set; } = null!;
        [Column("attachmenturl")]
        [StringLength(255)]
        public string Attachmenturl { get; set; } = null!;
        [Column("status")]
        [StringLength(255)]
        public string Status { get; set; } = null!;
        [Column("allowedfiletypes")]
        [StringLength(255)]
        public string Allowedfiletypes { get; set; } = null!;
        [Column("maxfilesizemb")]
        public double Maxfilesizemb { get; set; }

        public virtual Class FkClass { get; set; } = null!;
        public virtual Student FkStudent { get; set; } = null!;
    }
}
