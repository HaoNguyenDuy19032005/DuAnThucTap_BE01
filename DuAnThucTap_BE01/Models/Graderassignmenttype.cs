using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("graderassignmenttypes")]
    public partial class Graderassignmenttype
    {
        public Graderassignmenttype()
        {
            Exams = new HashSet<Exam>();
        }

        [Key]
        [Column("graderassignmenttypeid")]
        public Guid Graderassignmenttypeid { get; set; }
        [Column("typename")]
        [StringLength(255)]
        public string Typename { get; set; } = null!;

        [InverseProperty("Graderassignmenttype")]
        public virtual ICollection<Exam> Exams { get; set; }
    }
}
