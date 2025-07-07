using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("subjectsofexemption")]
    public partial class Subjectsofexemption
    {
        public Subjectsofexemption()
        {
            Studentexemptions = new HashSet<Studentexemption>();
        }

        [Key]
        [Column("objectid")]
        public Guid Objectid { get; set; }
        [Column("exemptionname")]
        [StringLength(255)]
        public string Exemptionname { get; set; } = null!;
        [Column("description")]
        public string? Description { get; set; }
        [Required]
        [Column("isactive")]
        public bool? Isactive { get; set; }

        [InverseProperty("Object")]
        public virtual ICollection<Studentexemption> Studentexemptions { get; set; }
    }
}
