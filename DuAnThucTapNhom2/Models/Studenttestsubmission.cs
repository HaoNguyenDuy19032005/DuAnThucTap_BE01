using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("studenttestsubmissions")]
    [Index("Submissionid", Name = "studenttestsubmissions_submissionid_key", IsUnique = true)]
    public partial class Studenttestsubmission
    {
        public Studenttestsubmission()
        {
            Studenttestanswers = new HashSet<Studenttestanswer>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("submissionid")]
        [StringLength(255)]
        public string Submissionid { get; set; } = null!;
        [Column("fk_assignmentid")]
        [StringLength(255)]
        public string FkAssignmentid { get; set; } = null!;
        [Column("fk_studentid")]
        [StringLength(255)]
        public string FkStudentid { get; set; } = null!;
        [Column("fk_teacherid")]
        [StringLength(255)]
        public string FkTeacherid { get; set; } = null!;
        [Column("starttime", TypeName = "timestamp without time zone")]
        public DateTime Starttime { get; set; }
        [Column("submissiontime", TypeName = "timestamp without time zone")]
        public DateTime Submissiontime { get; set; }
        [Column("status")]
        [StringLength(255)]
        public string Status { get; set; } = null!;
        [Column("score")]
        public double Score { get; set; }

        public virtual Testassignment FkAssignment { get; set; } = null!;
        public virtual Student FkStudent { get; set; } = null!;
        public virtual Teacher FkTeacher { get; set; } = null!;
        public virtual ICollection<Studenttestanswer> Studenttestanswers { get; set; }
    }
}
