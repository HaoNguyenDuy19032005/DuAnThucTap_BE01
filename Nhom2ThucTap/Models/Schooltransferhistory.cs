using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
{
    [Table("schooltransferhistory")]
    public partial class Schooltransferhistory
    {
        [Key]
        [Column("schooltransferid")]
        public int Schooltransferid { get; set; }

        [Required(ErrorMessage = "Không được để trống mã sinh viên.")]
        [Column("studentid")]
        public int Studentid { get; set; }

        [Required(ErrorMessage = "Không được để trống trường chuyển đi.")]
        [Column("fromschoolinfoid")]
        public int Fromschoolinfoid { get; set; }

        [Column("fromclassid")]
        public int? Fromclassid { get; set; }

        [Required(ErrorMessage = "Không được để trống trường chuyển đến.")]
        [Column("toschoolinfoid")]
        public int Toschoolinfoid { get; set; }

        [Column("toclassid")]
        public int? Toclassid { get; set; }

        [Required(ErrorMessage = "Không được để trống học kỳ.")]
        [Column("semesterid")]
        public int Semesterid { get; set; }

        [Required(ErrorMessage = "Không được để trống ngày chuyển.")]
        [Column("transferdate")]
        public DateOnly? Transferdate { get; set; }

        [Required(ErrorMessage = "Không được để trống lý do chuyển.")]
        [Column("reason")]
        public string? Reason { get; set; }

        [Column("attachmenturl")]
        public string? Attachmenturl { get; set; }

        [Required(ErrorMessage = "Không được để trống loại chuyển.")]
        [Column("transfertype")]
        public string? Transfertype { get; set; }

        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime? Createdat { get; set; }

        [ForeignKey("Studentid")]
        [JsonIgnore]
        public virtual Student? Student { get; set; } = null!;

        [ForeignKey("Fromclassid")]
        [JsonIgnore]
        public virtual Class? Fromclass { get; set; }

        [ForeignKey("Toclassid")]
        [JsonIgnore]
        public virtual Class? Toclass { get; set; }

        [ForeignKey("Semesterid")]
        [JsonIgnore]
        public virtual Semester? Semester { get; set; } = null!;
    }
}
