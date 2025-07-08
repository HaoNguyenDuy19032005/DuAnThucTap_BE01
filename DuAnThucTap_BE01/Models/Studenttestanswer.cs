using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("studenttestanswers")]
    [Index("Submissionid", "Questionid", Name = "studenttestanswers_submissionid_questionid_key", IsUnique = true)]
    public partial class Studenttestanswer
    {
        [Key]
        [Column("answerid")]
        public int Answerid { get; set; }
        [Column("submissionid")]
        public int Submissionid { get; set; }
        [Column("questionid")]
        public int Questionid { get; set; }
        [Column("answercontent")]
        public string? Answercontent { get; set; }
        [Column("selectedoption")]
        [StringLength(10)]
        public string? Selectedoption { get; set; }
        [Column("iscorrect")]
        public bool? Iscorrect { get; set; }

        [ForeignKey("Questionid")]
        [InverseProperty("Studenttestanswers")]
        public virtual Testquestion Question { get; set; } = null!;
        [ForeignKey("Submissionid")]
        [InverseProperty("Studenttestanswers")]
        public virtual Studenttestsubmission Submission { get; set; } = null!;
    }
}
