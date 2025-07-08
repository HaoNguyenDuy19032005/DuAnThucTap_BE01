using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("teacherconcurrentsubjects")]
    public partial class Teacherconcurrentsubject
    {
        [Key]
        [Column("teacherid")]
        public int Teacherid { get; set; }
        [Key]
        [Column("subjectid")]
        public int Subjectid { get; set; }
        [Key]
        [Column("schoolyearid")]
        public int Schoolyearid { get; set; }

        [ForeignKey("Schoolyearid")]
        [InverseProperty("Teacherconcurrentsubjects")]
        public virtual Schoolyear Schoolyear { get; set; } = null!;
        [ForeignKey("Subjectid")]
        [InverseProperty("Teacherconcurrentsubjects")]
        public virtual Subject Subject { get; set; } = null!;
        [ForeignKey("Teacherid")]
        [InverseProperty("Teacherconcurrentsubjects")]
        public virtual Teacher Teacher { get; set; } = null!;
    }
}
