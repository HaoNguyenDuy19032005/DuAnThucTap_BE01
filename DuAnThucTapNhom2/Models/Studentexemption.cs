using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("studentexemptions")]
    [Index("Studentexemptionid", Name = "studentexemptions_studentexemptionid_key", IsUnique = true)]
    public partial class Studentexemption
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("studentexemptionid")]
        [StringLength(255)]
        public string Studentexemptionid { get; set; } = null!;
        [Column("fk_studentid")]
        [StringLength(255)]
        public string FkStudentid { get; set; } = null!;

        public virtual Student FkStudent { get; set; } = null!;
    }
}
