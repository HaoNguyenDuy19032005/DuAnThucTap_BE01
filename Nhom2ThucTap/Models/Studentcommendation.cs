using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
{
    [Table("studentcommendations")]
    public partial class Studentcommendation
    {
        [Key]
        [Column("commendationid")]
        public int Commendationid { get; set; }

        [Required(ErrorMessage = "Không được để trống mã sinh viên.")]
        [Column("studentid")]
        public int Studentid { get; set; }

        [Required(ErrorMessage = "Không được để trống học kỳ.")]
        [Column("semesterid")]
        public int Semesterid { get; set; }

        [Required(ErrorMessage = "Không được để trống trường.")]
        [Column("schoolinfoid")]
        public int Schoolinfoid { get; set; }

        [Required(ErrorMessage = "Không được để trống loại khen thưởng.")]
        [Column("commendationtypeid")]
        public int Commendationtypeid { get; set; }

        [Required(ErrorMessage = "Không được để trống ngày khen thưởng.")]
        [Column("commendationdate")]
        public DateOnly? Commendationdate { get; set; }

        [Required(ErrorMessage = "Không được để trống nội dung.")]
        [Column("content")]
        public string? Content { get; set; }

        [Column("attachmenturl")]
        public string? Attachmenturl { get; set; }

        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime? Createdat { get; set; }

        [JsonIgnore]
        [ForeignKey("Commendationtypeid")]
        [InverseProperty("Studentcommendations")]
        public virtual Commendationtype? Commendationtype { get; set; } = null!;

        [JsonIgnore]
        [ForeignKey("Schoolinfoid")]
        [InverseProperty("Studentcommendations")]
        public virtual Schoolinformation? Schoolinfo { get; set; } = null!;

        [JsonIgnore]
        [ForeignKey("Semesterid")]
        [InverseProperty("Studentcommendations")]
        public virtual Semester? Semester { get; set; } = null!;

        [JsonIgnore]
        [ForeignKey("Studentid")]
        [InverseProperty("Studentcommendations")]
        public virtual Student? Student { get; set; } = null!;
    }
}
