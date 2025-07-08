using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("gradetypes")]
    public partial class Gradetype
    {
        public Gradetype()
        {
            Grades = new HashSet<Grade>();
        }

        [Key]
        [Column("gradetypeid")]
        public int Gradetypeid { get; set; }
        [Column("gradetypename")]
        [StringLength(100)]
        public string Gradetypename { get; set; } = null!;
        [Column("weightingfactor")]
        [Precision(5, 2)]
        public decimal Weightingfactor { get; set; }
        [Column("mininstancessemester1")]
        public int Mininstancessemester1 { get; set; }
        [Column("mininstancessemester2")]
        public int Mininstancessemester2 { get; set; }

        [InverseProperty("Gradetype")]
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
