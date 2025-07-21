using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
{
    [Table("teststudentanswers")]
    public class TestStudentAnswer
    {
        [Key]
        [Column("answerid")]
        public int AnswerId { get; set; }

        [Column("submissionid")]
        public int SubmissionId { get; set; }

        [Column("questionid")]
        public int? QuestionId { get; set; }

        [Column("selectedoption")]
        public string? SelectedOption { get; set; }

        [Column("answercontent")]
        public string? AnswerContent { get; set; }

        [Column("answerattachmenturl")]
        public string? AnswerAttachmentUrl { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        [ForeignKey("SubmissionId")]
        [InverseProperty("Answers")]
        public virtual TestStudentSubmission TestStudentSubmission { get; set; } = null!;

        [JsonIgnore]
        [ForeignKey("QuestionId")]
        [InverseProperty("StudentAnswers")]
        public virtual TestQuestionItem? TestQuestionItem { get; set; }
    }

}
