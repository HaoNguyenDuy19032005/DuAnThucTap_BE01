// Models/Teacherworkstatushistory.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace DuAnThucTap_BE01.Models
{
    [Table("teacherworkstatushistory")]
    public partial class Teacherworkstatushistory : IValidatableObject
    {
        [Key]
        [Column("historyid")]
        public int Historyid { get; set; }

        [Column("teacherid")]
        [Required(ErrorMessage = "ID Giáo viên không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Giáo viên không hợp lệ.")]
        public int Teacherid { get; set; }

        [Column("statustype")]
        [Required(ErrorMessage = "Loại trạng thái không được để trống.")]
        [StringLength(100)]
        public string Statustype { get; set; } = null!;

        [Column("startdate")]
        [Required(ErrorMessage = "Ngày bắt đầu không được để trống.")]
        public DateOnly? Startdate { get; set; }

        [Column("enddate")]
        public DateOnly? Enddate { get; set; }

        [Column("note")]
        public string? Note { get; set; }

        [Column("decisionfileurl")]
        [Url(ErrorMessage = "Đường dẫn tệp quyết định không hợp lệ.")]
        public string? Decisionfileurl { get; set; }

        [Column("createdat")]
        public DateTime? Createdat { get; set; }

        [ForeignKey("Teacherid")]
        [InverseProperty("Teacherworkstatushistories")]
        [JsonIgnore]
        public virtual Teacher? Teacher { get; set; }
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