using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("studenttestsubmissions")]
    [Index("Assignmentid", "Studentid", Name = "studenttestsubmissions_assignmentid_studentid_key", IsUnique = true)]
    public partial class Studenttestsubmission
    {
        public Studenttestsubmission()
        {
            Studenttestanswers = new HashSet<Studenttestanswer>();
            Submissionfiles = new HashSet<Submissionfile>();
        }

        [Key]
        [Column("submissionid")]
        public Guid Submissionid { get; set; }
        [Column("assignmentid")]
        public Guid Assignmentid { get; set; }
        [Column("studentid")]
        public Guid Studentid { get; set; }
        [Column("starttime")]
        public DateTime? Starttime { get; set; }
        [Column("submissiontime")]
        public DateTime? Submissiontime { get; set; }
        [Column("status")]
        [StringLength(50)]
        public string? Status { get; set; }
        [Column("score")]
        [Precision(5, 2)]
        public decimal? Score { get; set; }
        [Column("teacherfeedback")]
        public string? Teacherfeedback { get; set; }

        [ForeignKey("Assignmentid")]
        [InverseProperty("Studenttestsubmissions")]
        public virtual Testassignment Assignment { get; set; } = null!;
        [ForeignKey("Studentid")]
        [InverseProperty("Studenttestsubmissions")]
        public virtual Student Student { get; set; } = null!;
        [InverseProperty("Submission")]
        public virtual ICollection<Studenttestanswer> Studenttestanswers { get; set; }
        [InverseProperty("Submission")]
        public virtual ICollection<Submissionfile> Submissionfiles { get; set; }
    }
}
