using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("teachingassignments")]
    public partial class Teachingassignment
    {
        [Key]
        [Column("assignmentid")]
        public int Assignmentid { get; set; }
        [Column("teacherid")]
        public int Teacherid { get; set; }
        [Column("subjectid")]
        public int Subjectid { get; set; }
        [Column("classtypeid")]
        public int? Classtypeid { get; set; }
        [Column("topicid")]
        public int? Topicid { get; set; }
        [Column("schoolyearid")]
        public int Schoolyearid { get; set; }
        [Column("teachingstartdate")]
        public DateOnly? Teachingstartdate { get; set; }
        [Column("teachingenddate")]
        public DateOnly? Teachingenddate { get; set; }
        [Column("notes")]
        public string? Notes { get; set; }

        [ForeignKey("Classtypeid")]
        [InverseProperty("Teachingassignments")]
        public virtual Classtype? Classtype { get; set; }
        [ForeignKey("Schoolyearid")]
        [InverseProperty("Teachingassignments")]
        public virtual Schoolyear Schoolyear { get; set; } = null!;
        [ForeignKey("Subjectid")]
        [InverseProperty("Teachingassignments")]
        public virtual Subject Subject { get; set; } = null!;
        [ForeignKey("Teacherid")]
        [InverseProperty("Teachingassignments")]
        public virtual Teacher Teacher { get; set; } = null!;
        [ForeignKey("Topicid")]
        [InverseProperty("Teachingassignments")]
        public virtual Topiclist? Topic { get; set; }
    }
}
