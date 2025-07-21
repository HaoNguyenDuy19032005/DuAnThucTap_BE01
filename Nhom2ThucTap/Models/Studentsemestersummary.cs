using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
{
    [Table("studentsemestersummary")]
    public partial class Studentsemestersummary
    {
        [Key]
        [Column("summaryid")]
        public int Summaryid { get; set; }

        [Required(ErrorMessage = "Mã sinh viên không được để trống.")]
        [Column("studentid")]
        public int? Studentid { get; set; }

        [Required(ErrorMessage = "Mã học kỳ không được để trống.")]
        [Column("semesterid")]
        public int? Semesterid { get; set; }

        [Required(ErrorMessage = "Mã trường không được để trống.")]
        [Column("schoolinfoid")]
        public int? Schoolinfoid { get; set; }

        // ❌ KHÔNG Required: sẽ được tính tự động bằng trigger
        [Column("averagescore")]
        public double? Averagescore { get; set; }

        // ❌ KHÔNG Required: trigger tự gán
        [StringLength(50)]
        [Column("performancerating")]
        public string? Performancerating { get; set; }

        [StringLength(50)]
        [Column("conductrating")]
        public string? Conductrating { get; set; }

        [Column("calculateddate")]
        public DateTime? Calculateddate { get; set; }

        // Quan hệ
        [JsonIgnore]
        [ForeignKey("Schoolinfoid")]
        [InverseProperty("Studentsemestersummaries")]
        public virtual Schoolinformation? Schoolinfo { get; set; }

        [JsonIgnore]
        [ForeignKey("Semesterid")]
        [InverseProperty("Studentsemestersummaries")]
        public virtual Semester? Semester { get; set; }

        [JsonIgnore]
        [ForeignKey("Studentid")]
        [InverseProperty("Studentsemestersummaries")]
        public virtual Student? Student { get; set; }
    }
}
