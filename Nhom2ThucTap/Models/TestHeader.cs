using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
{
    [Table("testheaders")]
    public class TestHeader
    {
        public TestHeader()
        {
            TestQuestions = new HashSet<TestQuestionItem>();
            StudentSubmissions = new HashSet<TestStudentSubmission>();
        }

        [Key]
        [Column("testid")]
        public int TestId { get; set; }

        [Column("subjectid")]
        public int SubjectId { get; set; }

        [Column("classid")]
        public int ClassId { get; set; }

        [Column("title")]
        public string Title { get; set; } = null!;

        [Column("testformat")]
        public string TestFormat { get; set; } = null!;

        [Column("durationinminutes")]
        public int DurationInMinutes { get; set; }

        [Column("starttime")]
        public DateTime StartTime { get; set; }

        [Column("attachmenturl")]
        public string? AttachmentUrl { get; set; }

        [Column("requirestudentattachment")]
        public bool RequireStudentAttachment { get; set; }

        [Column("submissionrules")]
        public string? SubmissionRules { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        [InverseProperty("Test")]
        public virtual ICollection<TestQuestionItem> TestQuestions { get; set; }

        [JsonIgnore]
        [InverseProperty("Test")]
        public virtual ICollection<TestStudentSubmission> StudentSubmissions { get; set; }
    }
}
