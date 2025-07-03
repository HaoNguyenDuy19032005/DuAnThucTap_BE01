using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("studenttestanswers")]
    [Index("Answerid", Name = "studenttestanswers_answerid_key", IsUnique = true)]
    public partial class Studenttestanswer
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("answerid")]
        [StringLength(255)]
        public string Answerid { get; set; } = null!;
        [Column("fk_submissionid")]
        [StringLength(255)]
        public string FkSubmissionid { get; set; } = null!;
        [Column("filename")]
        [StringLength(255)]
        public string Filename { get; set; } = null!;
        [Column("fileurl")]
        [StringLength(255)]
        public string Fileurl { get; set; } = null!;
        [Column("filesizekb")]
        public double Filesizekb { get; set; }
        [Column("answercontent")]
        public string Answercontent { get; set; } = null!;
        [Column("selectedoption")]
        [StringLength(255)]
        public string Selectedoption { get; set; } = null!;
        [Column("iscorrect")]
        public bool Iscorrect { get; set; }

        public virtual Studenttestsubmission FkSubmission { get; set; } = null!;
    }
}
