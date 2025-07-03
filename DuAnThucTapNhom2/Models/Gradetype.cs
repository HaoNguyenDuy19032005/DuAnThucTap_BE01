using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("gradetypes")]
    [Index("Gradetypeid", Name = "gradetypes_gradetypeid_key", IsUnique = true)]
    public partial class Gradetype
    {
        public Gradetype()
        {
            Grades = new HashSet<Grade>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("gradetypeid")]
        [StringLength(255)]
        public string Gradetypeid { get; set; } = null!;
        [Column("gradetypename")]
        [StringLength(255)]
        public string Gradetypename { get; set; } = null!;
        [Column("weightingfactor")]
        public double Weightingfactor { get; set; }
        [Column("mininstancessemester1")]
        public int Mininstancessemester1 { get; set; }
        [Column("mininstancessemester2")]
        public int Mininstancessemester2 { get; set; }
        [Column("updatedat", TypeName = "timestamp without time zone")]
        public DateTime Updatedat { get; set; }
        [Required]
        [Column("isactive")]
        public bool? Isactive { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }
    }
}
