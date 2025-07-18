// Models/Teachertraininghistory.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace DuAnThucTap_BE01.Models
{
    [Table("teachertraininghistory")]
    public partial class Teachertraininghistory : IValidatableObject
    {
        [Key]
        [Column("trainingid")]
        public int Trainingid { get; set; }

        [Column("teacherid")]
        [Required(ErrorMessage = "ID giáo viên không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID giáo viên không hợp lệ.")]
        public int Teacherid { get; set; }

        [Column("traininginstitutionname")]
        [Required(ErrorMessage = "Tên cơ sở đào tạo không được để trống.")]
        [StringLength(255)]
        public string? Traininginstitutionname { get; set; }

        [Column("majororspecialization")]
        [Required(ErrorMessage = "Chuyên ngành không được để trống.")]
        [StringLength(255)]
        public string? Majororspecialization { get; set; }

        [Column("startdate")]
        [Required(ErrorMessage = "Ngày bắt đầu không được để trống.")]
        public DateOnly? Startdate { get; set; }

        [Column("enddateorgraduationyear")]
        [Required(ErrorMessage = "Ngày kết thúc/Năm tốt nghiệp không được để trống.")]
        [StringLength(50)]
        public string? Enddateorgraduationyear { get; set; }

        [Column("active")]
        public bool? Active { get; set; }

        [Column("trainingtype")]
        [Required(ErrorMessage = "Loại hình đào tạo không được để trống.")]
        [StringLength(100)]
        public string? Trainingtype { get; set; }

        [Column("certificatediplomaname")]
        [Required(ErrorMessage = "Tên văn bằng/chứng chỉ không được để trống.")]
        [StringLength(255)]
        public string? Certificatediplomaname { get; set; }

        [Column("attachmenturl")]
        [Url(ErrorMessage = "Đường dẫn tệp đính kèm không hợp lệ.")]
        public string? Attachmenturl { get; set; }

        [ForeignKey("Teacherid")]
        [InverseProperty("Teachertraininghistories")]
        [JsonIgnore]
        public virtual Teacher? Teacher { get; set; }

        // Custom validation
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Startdate.HasValue && Startdate.Value > DateOnly.FromDateTime(DateTime.Today))
            {
                yield return new ValidationResult(
                    "Ngày bắt đầu không được là một ngày trong tương lai.",
                    new[] { nameof(Startdate) }
                );
            }
        }
    }
}