using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("testquestions")]
    [Index("Questionid", Name = "testquestions_questionid_key", IsUnique = true)]
    public partial class Testquestion
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("questionid")]
        [StringLength(255)]
        public string Questionid { get; set; } = null!;
        [Column("fk_testid")]
        [StringLength(255)]
        public string FkTestid { get; set; } = null!;

        public virtual Test FkTest { get; set; } = null!;
        public virtual Submissionfile? Submissionfile { get; set; }
    }
}
