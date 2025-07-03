using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("submissionfiles")]
    [Index("FkQuestionid", Name = "submissionfiles_fk_questionid_key", IsUnique = true)]
    public partial class Submissionfile
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("fk_questionid")]
        [StringLength(255)]
        public string FkQuestionid { get; set; } = null!;
        [Column("answercontent")]
        public string Answercontent { get; set; } = null!;
        [Column("selectedoption")]
        [StringLength(255)]
        public string Selectedoption { get; set; } = null!;
        [Column("iscorrect")]
        public bool Iscorrect { get; set; }

        public virtual Testquestion FkQuestion { get; set; } = null!;
    }
}
