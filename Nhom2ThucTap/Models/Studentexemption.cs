using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
{
    [Table("studentexemptions")]
    public partial class Studentexemption
    {
        [Key]
        [Column("studentexemptionid")]
        public int Studentexemptionid { get; set; }

        [Required(ErrorMessage = "Mã sinh viên không được để trống.")]
        [Column("studentid")]
        public int Studentid { get; set; }

        [Required(ErrorMessage = "Đối tượng miễn giảm không được để trống.")]
        [Column("objectid")]
        public int Objectid { get; set; }

        [Required(ErrorMessage = "Hình thức miễn giảm không được để trống.")]
        [MaxLength(255, ErrorMessage = "Hình thức miễn giảm không được quá 255 ký tự.")]
        [Column("formofexemption")]
        public string? Formofexemption { get; set; }

        [JsonIgnore]
        [ForeignKey("Objectid")]
        [InverseProperty("Studentexemptions")]
        public virtual Subjectsofexemption? Object { get; set; } = null!;

        [JsonIgnore]
        [ForeignKey("Studentid")]
        [InverseProperty("Studentexemptions")]
        public virtual Student? Student { get; set; } = null!;
    }
}
