using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
{
    [Table("studentsubjectsummary")]
    public class Studentsubjectsummary
    {
        [Key]
        [Column("subjectsummaryid")]
        public int Subjectsummaryid { get; set; }

        [Required(ErrorMessage = "Mã học sinh không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "Mã học sinh phải lớn hơn 0.")]
        [Column("studentid")]
        public int? Studentid { get; set; }

        [Required(ErrorMessage = "Mã môn học không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "Mã môn học phải lớn hơn 0.")]
        [Column("subjectid")]
        public int? Subjectid { get; set; }

        [Required(ErrorMessage = "Mã học kỳ không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "Mã học kỳ phải lớn hơn 0.")]
        [Column("semesterid")]
        public int? Semesterid { get; set; }

        [Required(ErrorMessage = "Điểm trung bình không được để trống.")]
        [Range(0, 10, ErrorMessage = "Điểm trung bình phải từ 0 đến 10.")]
        [Column("averagescore")]
        public double? Averagescore { get; set; }

        [Required(ErrorMessage = "Mã trường không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "Mã trường phải lớn hơn 0.")]
        [Column("schoolinfoid")]
        public int? Schoolinfoid { get; set; }

        // 🔗 Navigation properties (optional)
        [JsonIgnore]
        [ForeignKey("Studentid")]
        public virtual Student? Student { get; set; }

        [JsonIgnore]
        [ForeignKey("Subjectid")]
        public virtual Subject? Subject { get; set; }

        [JsonIgnore]
        [ForeignKey("Semesterid")]
        public virtual Semester? Semester { get; set; }

        [JsonIgnore]
        [ForeignKey("Schoolinfoid")]
        public virtual Schoolinformation? Schoolinfo { get; set; }
    }
}
