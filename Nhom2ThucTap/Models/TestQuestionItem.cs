using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
{
    [Table("testquestionitems")]
    public class TestQuestionItem
    {
        public TestQuestionItem()
        {
            StudentAnswers = new HashSet<TestStudentAnswer>();
        }

        [Key]
        [Column("questionid")]
        public int QuestionId { get; set; }

        [Column("testid")]
        public int TestId { get; set; }

        [Column("displayorder")]
        public int DisplayOrder { get; set; }

        [Column("content")]
        public string Content { get; set; } = null!;

        [Column("optiona")]
        public string? OptionA { get; set; }

        [Column("optionb")]
        public string? OptionB { get; set; }

        [Column("optionc")]
        public string? OptionC { get; set; }

        [Column("optiond")]
        public string? OptionD { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime? UpdatedAt { get; set; }


        [JsonIgnore]
        [ForeignKey("TestId")]
        [InverseProperty("TestQuestions")]
        public virtual TestHeader Test { get; set; } = null!;

        [JsonIgnore]
        [InverseProperty("TestQuestionItem")]
        public virtual ICollection<TestStudentAnswer> StudentAnswers { get; set; }
    }
}
