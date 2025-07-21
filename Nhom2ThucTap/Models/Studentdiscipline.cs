using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
{
    [Table("studentdisciplines")]
    public partial class Studentdiscipline
    {
        [Key]
        [Column("disciplineid")]
        public int Disciplineid { get; set; }

        [Required(ErrorMessage = "Mã sinh viên không được để trống.")]
        [Column("studentid")]
        public int Studentid { get; set; }

        [Required(ErrorMessage = "Học kỳ không được để trống.")]
        [Column("semesterid")]
        public int Semesterid { get; set; }

        [Required(ErrorMessage = "Trường không được để trống.")]
        [Column("schoolinfoid")]
        public int Schoolinfoid { get; set; }

        [Required(ErrorMessage = "Loại kỷ luật không được để trống.")]
        [Column("disciplinetypeid")]
        public int Disciplinetypeid { get; set; }

        [Column("commendationdate")]
        public DateOnly? Commendationdate { get; set; }

        [MaxLength(255, ErrorMessage = "Nội dung không được vượt quá 255 ký tự.")]
        [Column("content")]
        public string? Content { get; set; }

        [MaxLength(500, ErrorMessage = "Đường dẫn tệp đính kèm không được vượt quá 500 ký tự.")]
        [Column("attachmenturl")]
        public string? Attachmenturl { get; set; }

        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime? Createdat { get; set; }

        [JsonIgnore]
        [ForeignKey("Disciplinetypeid")]
        [InverseProperty("Studentdisciplines")]
        public virtual Disciplinetype? Disciplinetype { get; set; } = null!;

        [JsonIgnore]
        [ForeignKey("Schoolinfoid")]
        [InverseProperty("Studentdisciplines")]
        public virtual Schoolinformation? Schoolinfo { get; set; } = null!;

        [JsonIgnore]
        [ForeignKey("Semesterid")]
        [InverseProperty("Studentdisciplines")]
        public virtual Semester? Semester { get; set; } = null!;

        [JsonIgnore]
        [ForeignKey("Studentid")]
        [InverseProperty("Studentdisciplines")]
        public virtual Student? Student { get; set; } = null!;
    }
}
