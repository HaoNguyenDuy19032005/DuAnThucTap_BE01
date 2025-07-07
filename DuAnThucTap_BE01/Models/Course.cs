using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("courses")]
    public partial class Course
    {
        public Course()
        {
            Lessons = new HashSet<Lesson>();
        }

        [Key]
        [Column("courseid")]
        public Guid Courseid { get; set; }
        [Column("coursename")]
        [StringLength(255)]
        public string Coursename { get; set; } = null!;
        [Column("teacherid")]
        public Guid? Teacherid { get; set; }
        [Column("thumbnailurl")]
        public string? Thumbnailurl { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("startdate")]
        public DateOnly? Startdate { get; set; }
        [Column("enddate")]
        public DateOnly? Enddate { get; set; }
        [Column("maxstudents")]
        public int? Maxstudents { get; set; }
        [Column("price")]
        [Precision(12, 2)]
        public decimal? Price { get; set; }

        [ForeignKey("Teacherid")]
        [InverseProperty("Courses")]
        public virtual Teacher? Teacher { get; set; }
        [InverseProperty("Course")]
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
