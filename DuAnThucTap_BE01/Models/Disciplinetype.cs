using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("disciplinetypes")]
    public partial class Disciplinetype
    {
        public Disciplinetype()
        {
            Studentdisciplines = new HashSet<Studentdiscipline>();
        }

        [Key]
        [Column("disciplinetypeid")]
        public int Disciplinetypeid { get; set; }
        [Column("typename")]
        [StringLength(255)]
        public string Typename { get; set; } = null!;
        [Column("severity")]
        [StringLength(100)]
        public string? Severity { get; set; }

        [InverseProperty("Disciplinetype")]
        public virtual ICollection<Studentdiscipline> Studentdisciplines { get; set; }
    }
}
