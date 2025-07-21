using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
{
    [Table("studentpreservations")]
    public partial class Studentpreservation
    {
        [Key]
        [Column("preservationid")]
        public int Preservationid { get; set; }

        [Required(ErrorMessage = "Mã sinh viên không được để trống.")]
        [Column("studentid")]
        public int Studentid { get; set; }

        [Required(ErrorMessage = "Mã lớp không được để trống.")]
        [Column("classid")]
        public int Classid { get; set; }

        [Required(ErrorMessage = "Ngày bảo lưu không được để trống.")]
        [Column("preservationdate")]
        public DateOnly Preservationdate { get; set; }

        [Required(ErrorMessage = "Mã học kỳ không được để trống.")]
        [Column("semesterid")]
        public int Semesterid { get; set; }

        [Required(ErrorMessage = "Thời gian bảo lưu không được để trống.")]
        [Column("preservationduration")]
        public string Preservationduration { get; set; } = null!;

        [Required(ErrorMessage = "Lý do không được để trống.")]
        [Column("reason")]
        public string Reason { get; set; } = null!;

        [Column("attachmenturl")]
        public string? Attachmenturl { get; set; }

        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime? Createdat { get; set; }

        [Column("updatedat", TypeName = "timestamp without time zone")]
        public DateTime? Updatedat { get; set; }

        [JsonIgnore]
        [ForeignKey("Classid")]
        [InverseProperty("Studentpreservations")]
        public virtual Class? Class { get; set; } = null!;

        [JsonIgnore]
        [ForeignKey("Semesterid")]
        [InverseProperty("Studentpreservations")]
        public virtual Semester? Semester { get; set; } = null!;

        [JsonIgnore]
        [ForeignKey("Studentid")]
        [InverseProperty("Studentpreservations")]
        public virtual Student? Student { get; set; } = null!;
    }
}
