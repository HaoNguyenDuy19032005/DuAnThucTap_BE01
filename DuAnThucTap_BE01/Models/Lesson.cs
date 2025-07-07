using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("lessons")]
    public partial class Lesson
    {
        [Key]
        [Column("lessonid")]
        public Guid Lessonid { get; set; }
        [Column("courseid")]
        public Guid Courseid { get; set; }
        [Column("teacherid")]
        public Guid Teacherid { get; set; }
        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; } = null!;
        [Column("description")]
        public string? Description { get; set; }
        [Column("starttime")]
        public DateTime? Starttime { get; set; }
        [Column("endtime")]
        public DateTime? Endtime { get; set; }
        [Column("durationinminutes")]
        public int? Durationinminutes { get; set; }
        [Column("password")]
        [StringLength(50)]
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
        [StringLength(100)]
        public string? Meetingid { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }

        [ForeignKey("Courseid")]
        [InverseProperty("Lessons")]
        public virtual Course Course { get; set; } = null!;
        [ForeignKey("Teacherid")]
        [InverseProperty("Lessons")]
        public virtual Teacher Teacher { get; set; } = null!;
    }
}
