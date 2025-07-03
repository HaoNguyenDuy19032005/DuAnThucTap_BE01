using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("lessons")]
    [Index("Lessonid", Name = "lessons_lessonid_key", IsUnique = true)]
    public partial class Lesson
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("lessonid")]
        [StringLength(255)]
        public string Lessonid { get; set; } = null!;
        [Column("fk_teacherid")]
        [StringLength(255)]
        public string FkTeacherid { get; set; } = null!;
        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; } = null!;
        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; } = null!;
        [Column("starttime", TypeName = "timestamp without time zone")]
        public DateTime Starttime { get; set; }
        [Column("endtime", TypeName = "timestamp without time zone")]
        public DateTime Endtime { get; set; }
        [Column("durationinminutes")]
        public int Durationinminutes { get; set; }
        [Column("password")]
        [StringLength(255)]
        public string Password { get; set; } = null!;
        [Column("autostartontime")]
        public bool Autostartontime { get; set; }
        [Column("isrecordingenabled")]
        public bool Isrecordingenabled { get; set; }
        [Column("allowstudentsharing")]
        public bool Allowstudentsharing { get; set; }
        [Column("shareablelink")]
        [StringLength(255)]
        public string Shareablelink { get; set; } = null!;
        [Column("meetingid")]
        [StringLength(255)]
        public string Meetingid { get; set; } = null!;
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime Createdat { get; set; }
        [Column("fk_courseid")]
        [StringLength(255)]
        public string FkCourseid { get; set; } = null!;

        public virtual Course FkCourse { get; set; } = null!;
        public virtual Teacher FkTeacher { get; set; } = null!;
    }
}
