
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
{
    [Table("teststudentsubmissions")]
    public class TestStudentSubmission
    {
        [Key]
        [Column("submissionid")]
        public int SubmissionId { get; set; }

        [Column("testid")]
        public int TestId { get; set; }

        [Column("studentid")]
        public int StudentId { get; set; }

        [Column("starttime", TypeName = "date")]
        public DateTime? StartTime { get; set; }

        [Column("submissiontime", TypeName = "date")]
        public DateTime? SubmissionTime { get; set; }

        [Column("status")]
        public string? Status { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        [ForeignKey("StudentId")]
        [InverseProperty("StudentSubmissions")]
        public virtual Student Student { get; set; } = null!;

        [JsonIgnore]
        [ForeignKey("TestId")]
        [InverseProperty("StudentSubmissions")]
        public virtual TestHeader Test { get; set; } = null!;

        [JsonIgnore]
        [InverseProperty("TestStudentSubmission")]
        public virtual ICollection<TestStudentAnswer> Answers { get; set; } = new HashSet<TestStudentAnswer>();
    }

}