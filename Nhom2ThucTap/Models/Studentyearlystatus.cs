using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
{
    [Table("studentyearlystatus")]
    public partial class Studentyearlystatus
    {
        [Key]
        [Column("studentyearlystatusid")]
        public int Studentyearlystatusid { get; set; }

        [Required(ErrorMessage = "studentid không được để trống")]
        [Column("studentid")]
        public int? Studentid { get; set; }

        [Required(ErrorMessage = "schoolyearid không được để trống")]
        [Column("schoolyearid")]
        public int? Schoolyearid { get; set; }

        [Required(ErrorMessage = "classid không được để trống")]
        [Column("classid")]
        public int? Classid { get; set; }

        [Required(ErrorMessage = "gradelevelid không được để trống")]
        [Column("gradelevelid")]
        public int? Gradelevelid { get; set; }

        [Required(ErrorMessage = "enrollmentstatus không được để trống")]
        [Column("enrollmentstatus")]
        public string? Enrollmentstatus { get; set; }

        [Column("enrollmentdate")]
        public DateOnly? Enrollmentdate { get; set; }

        [Column("graduationdate")]
        public DateOnly? Graduationdate { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime? Createdat { get; set; }

        [Column("updatedat", TypeName = "timestamp without time zone")]
        public DateTime? Updatedat { get; set; }

        [ForeignKey("Classid")]
        [JsonIgnore]
        public virtual Class? Class { get; set; }

        [ForeignKey("Gradelevelid")]
        [JsonIgnore]
        public virtual Gradelevel? Gradelevel { get; set; }

        [ForeignKey("Schoolyearid")]
        [JsonIgnore]
        public virtual Schoolyear? Schoolyear { get; set; }

        [ForeignKey("Studentid")]
        [JsonIgnore]
        public virtual Student? Student { get; set; }
    }
}
