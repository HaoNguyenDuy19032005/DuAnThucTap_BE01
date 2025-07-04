using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("submissionfiles")]
    public partial class Submissionfile
    {
        [Key]
        [Column("fileid")]
        public Guid Fileid { get; set; }
        [Column("submissionid")]
        public Guid Submissionid { get; set; }
        [Column("filename")]
        [StringLength(255)]
        public string? Filename { get; set; }
        [Column("fileurl")]
        public string Fileurl { get; set; } = null!;
        [Column("filesizekb")]
        public int? Filesizekb { get; set; }

        [ForeignKey("Submissionid")]
        [InverseProperty("Submissionfiles")]
        public virtual Studenttestsubmission Submission { get; set; } = null!;
    }
}
