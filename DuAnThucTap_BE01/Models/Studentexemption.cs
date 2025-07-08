using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("studentexemptions")]
    public partial class Studentexemption
    {
        [Key]
        [Column("studentexemptionid")]
        public int Studentexemptionid { get; set; }
        [Column("studentid")]
        public int Studentid { get; set; }
        [Column("objectid")]
        public int Objectid { get; set; }
        [Column("formofexemption")]
        [StringLength(255)]
        public string? Formofexemption { get; set; }

        [ForeignKey("Objectid")]
        [InverseProperty("Studentexemptions")]
        public virtual Subjectsofexemption Object { get; set; } = null!;
        [ForeignKey("Studentid")]
        [InverseProperty("Studentexemptions")]
        public virtual Student Student { get; set; } = null!;
    }
}
