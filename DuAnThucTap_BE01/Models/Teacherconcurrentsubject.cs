using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization; // Thêm dòng này

namespace DuAnThucTap_BE01.Models
{
    [Table("teacherconcurrentsubjects")]
    public partial class Teacherconcurrentsubject
    {
        [Key]
        [Column("teacherid")]
        public int Teacherid { get; set; }
        [Key]
        [Column("subjectid")]
        public int Subjectid { get; set; }
        [Key]
        [Column("schoolyearid")]
        public int Schoolyearid { get; set; }

        [ForeignKey("Schoolyearid")]
        [InverseProperty("Teacherconcurrentsubjects")]
        [JsonIgnore] // <-- Sửa
        public virtual Schoolyear? Schoolyear { get; set; }

        [ForeignKey("Subjectid")]
        [InverseProperty("Teacherconcurrentsubjects")]
        [JsonIgnore] // <-- Sửa
        public virtual Subject? Subject { get; set; }

        [ForeignKey("Teacherid")]
        [InverseProperty("Teacherconcurrentsubjects")]
        [JsonIgnore] // <-- Sửa
        public virtual Teacher? Teacher { get; set; }
    }
}