using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhom2ThucTap.Models
{
    [Table("classtypes")]
    public partial class Classtype
    {
        public Classtype()
        {
            Classes = new HashSet<Class>();
            Exams = new HashSet<Exam>();
        }

        [Key]
        [Column("classtypeid")]
        public int Classtypeid { get; set; }
        [Column("classtypename")]
        [StringLength(100)]
        public string Classtypename { get; set; } = null!;
        [Required]
        [Column("isactive")]
        public bool? Isactive { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }
        [Column("updatedat")]
        public DateTime? Updatedat { get; set; }

        [InverseProperty("Classtype")]
        public virtual ICollection<Class> Classes { get; set; }
        [InverseProperty("Classtype")]
        public virtual ICollection<Exam> Exams { get; set; }
    }
}
