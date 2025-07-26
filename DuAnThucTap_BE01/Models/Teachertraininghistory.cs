using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace DuAnThucTap_BE01.Models
{
    [Table("teachertraininghistory")]
    // Đã bỏ IValidatableObject
    public partial class Teachertraininghistory
    {
        [Key]
        [Column("trainingid")]
        public int Trainingid { get; set; }

        [Column("teacherid")]
        public int Teacherid { get; set; }

        [Column("traininginstitutionname")]
        [StringLength(255)] // Giữ lại StringLength để khớp với database
        public string? Traininginstitutionname { get; set; }

        [Column("majororspecialization")]
        [StringLength(255)] // Giữ lại StringLength để khớp với database
        public string? Majororspecialization { get; set; }

        [Column("startdate")]
        public DateOnly? Startdate { get; set; }

        [Column("enddateorgraduationyear")]
        [StringLength(50)] // Giữ lại StringLength để khớp với database
        public string? Enddateorgraduationyear { get; set; }

        [Column("active")]
        public bool? Active { get; set; }

        [Column("trainingtype")]
        [StringLength(100)] // Giữ lại StringLength để khớp với database
        public string? Trainingtype { get; set; }

        [Column("certificatediplomaname")]
        [StringLength(255)] // Giữ lại StringLength để khớp với database
        public string? Certificatediplomaname { get; set; }

        [Column("attachmenturl")]
        public string? Attachmenturl { get; set; }

        [ForeignKey("Teacherid")]
        [InverseProperty("Teachertraininghistories")]
        [JsonIgnore]
        public virtual Teacher? Teacher { get; set; }

        // Đã xóa toàn bộ phương thức Validate()
    }
}