// Models/Teacherworkhistory.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace DuAnThucTap_BE01.Models
{
    [Table("teacherworkhistory")]
    public partial class Teacherworkhistory : IValidatableObject
    {
        [Key]
        [Column("workhistoryid")]
        public int Workhistoryid { get; set; }

        [Column("teacherid")]
        [Required(ErrorMessage = "ID Giáo viên không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Giáo viên không hợp lệ.")]
        public int Teacherid { get; set; }

        [Column("operationunitid")]
        public int? Operationunitid { get; set; }

        [Column("departmentid")]
        public int? Departmentid { get; set; }

        [Column("iscurrentschool")]
        public bool? Iscurrentschool { get; set; }

        [Column("positionheld")]
        [Required(ErrorMessage = "Vị trí công tác không được để trống.")]
        [StringLength(150)]
        public string? Positionheld { get; set; }

        [Column("startdate")]
        [Required(ErrorMessage = "Ngày bắt đầu không được để trống.")]
        public DateOnly? Startdate { get; set; }

        [Column("enddate")]
        public DateOnly? Enddate { get; set; } // Có thể null nếu là công việc hiện tại

        [ForeignKey("Departmentid")]
        [InverseProperty("Teacherworkhistories")]
        [JsonIgnore]
        public virtual Department? Department { get; set; }

        [ForeignKey("Operationunitid")]
        [InverseProperty("Teacherworkhistories")]
        [JsonIgnore]
        public virtual Operationunit? Operationunit { get; set; }

        [ForeignKey("Teacherid")]
        [InverseProperty("Teacherworkhistories")]
        [JsonIgnore]
        public virtual Teacher? Teacher { get; set; }

        // Custom validation: Ngày kết thúc phải sau ngày bắt đầu
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Startdate.HasValue && Enddate.HasValue && Startdate.Value >= Enddate.Value)
            {
                yield return new ValidationResult(
                    "Ngày kết thúc phải sau ngày bắt đầu.",
                    new[] { nameof(Enddate) }
                );
            }
        }
    }
}