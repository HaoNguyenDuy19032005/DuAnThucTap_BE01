using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace DuAnThucTap_BE01.Models
{
    [Table("teacherconcurrentsubjects")]
    public partial class Teacherconcurrentsubject
    {
        [Key]
        [Column("teacherid")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Giáo viên không hợp lệ.")]
        public int Teacherid { get; set; }

        [Key]
        [Column("subjectid")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Môn học không hợp lệ.")]
        public int Subjectid { get; set; }

        [Key]
        [Column("schoolyearid")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Năm học không hợp lệ.")]
        public int Schoolyearid { get; set; }

        [ForeignKey("Schoolyearid")]
        [InverseProperty("Teacherconcurrentsubjects")]
        [JsonIgnore]
        public virtual Schoolyear? Schoolyear { get; set; }

        [ForeignKey("Subjectid")]
        [InverseProperty("Teacherconcurrentsubjects")]
        [JsonIgnore]
        public virtual Subject? Subject { get; set; }

        [ForeignKey("Teacherid")]
        [InverseProperty("Teacherconcurrentsubjects")]
        [JsonIgnore]
        public virtual Teacher? Teacher { get; set; }
    }
}