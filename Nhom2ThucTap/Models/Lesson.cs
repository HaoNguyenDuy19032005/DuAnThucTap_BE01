using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhom2ThucTap.Models
{
    [Table("lessons")]
    public partial class Lesson
    {
        [Key]
        [Column("lessonid")]
        public int Lessonid { get; set; }
        [Column("teacherid")]
        public int Teacherid { get; set; }
        [Column("title")]
        public string Title { get; set; } = null!;
        [Column("description")]
        public string? Description { get; set; }
        [Column("starttime", TypeName = "timestamp without time zone")]
        public DateTime? Starttime { get; set; }
        [Column("endtime", TypeName = "timestamp without time zone")]
        public DateTime? Endtime { get; set; }
        [Column("durationinminutes")]
        public int? Durationinminutes { get; set; }
        [Column("password")]
        public string? Password { get; set; }
        [Column("autostartontime")]
        public bool? Autostartontime { get; set; }
        [Column("isrecordingenabled")]
        public bool? Isrecordingenabled { get; set; }
        [Column("allowstudentsharing")]
        public bool? Allowstudentsharing { get; set; }
        [Column("shareablelink")]
        public string? Shareablelink { get; set; }
        [Column("meetingid")]
        public string? Meetingid { get; set; }
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime? Createdat { get; set; }
        [Column("courseid")]
        public int Courseid { get; set; }

        [ForeignKey("Courseid")]
        [InverseProperty("Lessons")]
        public virtual Course Course { get; set; } = null!;
        [ForeignKey("Teacherid")]
        [InverseProperty("Lessons")]
        public virtual Teacher Teacher { get; set; } = null!;
    }
}
