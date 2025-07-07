using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("examschedules")]
    [Index("Examid", "Classid", Name = "examschedules_examid_classid_key", IsUnique = true)]
    public partial class Examschedule
    {
        public Examschedule()
        {
            Examgraders = new HashSet<Examgrader>();
        }

        [Key]
        [Column("examscheduleid")]
        public Guid Examscheduleid { get; set; }
        [Column("examid")]
        public Guid Examid { get; set; }
        [Column("classid")]
        public Guid Classid { get; set; }

        [ForeignKey("Classid")]
        [InverseProperty("Examschedules")]
        public virtual Class Class { get; set; } = null!;
        [ForeignKey("Examid")]
        [InverseProperty("Examschedules")]
        public virtual Exam Exam { get; set; } = null!;
        [InverseProperty("Examschedule")]
        public virtual ICollection<Examgrader> Examgraders { get; set; }
    }
}
