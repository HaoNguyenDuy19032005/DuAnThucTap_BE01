//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

//namespace Nhom2ThucTap.Models
//{
//    [Table("studenttestsubmissions")]
//    public partial class Studenttestsubmission
//    {
//        public Studenttestsubmission()
//        {
//            Studenttestanswers = new HashSet<Studenttestanswer>();
//            Submissionfiles = new HashSet<Submissionfile>();
//        }

//        [Key]
//        [Column("submissionid")]
//        public int Submissionid { get; set; }
//        [Column("assignmentid")]
//        public int Assignmentid { get; set; }
//        [Column("studentid")]
//        public int Studentid { get; set; }
//        [Column("starttime", TypeName = "timestamp without time zone")]
//        public DateTime? Starttime { get; set; }
//        [Column("submissiontime", TypeName = "timestamp without time zone")]
//        public DateTime? Submissiontime { get; set; }
//        [Column("status")]
//        public string? Status { get; set; }
//        [Column("score")]
//        public decimal? Score { get; set; }
//        [Column("teacherfeedback")]
//        public string? Teacherfeedback { get; set; }

//        [ForeignKey("Assignmentid")]
//        [InverseProperty("Studenttestsubmissions")]
//        public virtual Testassignment Assignment { get; set; } = null!;
//        [ForeignKey("Studentid")]
//        [InverseProperty("Studenttestsubmissions")]
//        public virtual Student Student { get; set; } = null!;
//        [InverseProperty("Submission")]
//        public virtual ICollection<Studenttestanswer> Studenttestanswers { get; set; }
//        [InverseProperty("Submission")]
//        public virtual ICollection<Submissionfile> Submissionfiles { get; set; }
//    }
//}
