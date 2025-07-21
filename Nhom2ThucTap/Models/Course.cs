using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhom2ThucTap.Models
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
        public int Courseid { get; set; }
        [Column("coursename")]
        public string Coursename { get; set; } = null!;
        [Column("teacherid")]
        public int Teacherid { get; set; }
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
        public decimal? Price { get; set; }

        [ForeignKey("Teacherid")]
        [InverseProperty("Courses")]
        public virtual Teacher Teacher { get; set; } = null!;
        [InverseProperty("Course")]
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
