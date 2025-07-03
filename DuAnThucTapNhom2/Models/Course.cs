using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("courses")]
    [Index("Courseid", Name = "courses_courseid_key", IsUnique = true)]
    public partial class Course
    {
        public Course()
        {
            Lessons = new HashSet<Lesson>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("courseid")]
        [StringLength(255)]
        public string Courseid { get; set; } = null!;
        [Column("coursename")]
        [StringLength(255)]
        public string Coursename { get; set; } = null!;
        [Column("teacherid")]
        [StringLength(255)]
        public string? Teacherid { get; set; }
        [Column("thumbnailurl")]
        [StringLength(255)]
        public string Thumbnailurl { get; set; } = null!;
        [Column("description")]
        public string Description { get; set; } = null!;
        [Column("startdate", TypeName = "timestamp without time zone")]
        public DateTime Startdate { get; set; }
        [Column("enddate", TypeName = "timestamp without time zone")]
        public DateTime Enddate { get; set; }
        [Column("maxstudents")]
        public int Maxstudents { get; set; }
        [Column("price")]
        public double Price { get; set; }

        public virtual Teacher? Teacher { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
