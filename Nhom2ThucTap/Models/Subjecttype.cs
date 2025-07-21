using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhom2ThucTap.Models
{
    [Table("subjecttypes")]
    public partial class Subjecttype
    {
        public Subjecttype()
        {
            Subjects = new HashSet<Subject>();
        }

        [Key]
        [Column("subjecttypeid")]
        public int Subjecttypeid { get; set; }
        [Column("subjecttypename")]
        [StringLength(255)]
        public string Subjecttypename { get; set; } = null!;
        [Required]
        [Column("isactive")]
        public bool? Isactive { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }
        [Column("updatedat")]
        public DateTime? Updatedat { get; set; }

        [InverseProperty("Subjecttype")]
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
