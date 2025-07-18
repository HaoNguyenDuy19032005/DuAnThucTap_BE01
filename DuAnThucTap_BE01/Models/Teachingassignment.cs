using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Thêm thư viện này
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("teachingassignments")]
    public partial class Teachingassignment
    {
        [Key]
        [Column("assignmentid")]
        public int Assignmentid { get; set; }

        [Required(ErrorMessage = "Mã giáo viên không được để trống")]
        [Column("teacherid")]
        public int Teacherid { get; set; }

        [Required(ErrorMessage = "Mã môn học không được để trống")]
        [Column("subjectid")]
        public int Subjectid { get; set; }

        [Column("classtypeid")]
        public int? Classtypeid { get; set; }

        [Column("topicid")]
        public int? Topicid { get; set; }

        [Required(ErrorMessage = "Mã năm học không được để trống")]
        [Column("schoolyearid")]
        public int Schoolyearid { get; set; }

        [Column("teachingstartdate")]
        public DateOnly? Teachingstartdate { get; set; }

        [Column("teachingenddate")]
        public DateOnly? Teachingenddate { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        // ... các thuộc tính navigation giữ nguyên ...
        [JsonIgnore]
        [ForeignKey("Classtypeid")]
        [InverseProperty("Teachingassignments")]
        public virtual Classtype? Classtype { get; set; }

        [JsonIgnore]
        [ForeignKey("Schoolyearid")]
        [InverseProperty("Teachingassignments")]
        public virtual Schoolyear? Schoolyear { get; set; }

        [JsonIgnore]
        [ForeignKey("Subjectid")]
        [InverseProperty("Teachingassignments")]
        public virtual Subject? Subject { get; set; }

        [JsonIgnore]
        [ForeignKey("Teacherid")]
        [InverseProperty("Teachingassignments")]
        public virtual Teacher? Teacher { get; set; }

        [JsonIgnore]
        [ForeignKey("Topicid")]
        [InverseProperty("Teachingassignments")]
        public virtual Topiclist? Topic { get; set; }
    }
}