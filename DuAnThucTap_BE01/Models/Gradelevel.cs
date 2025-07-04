using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("gradelevels")]
    [Index("Codegradelevel", Name = "gradelevels_codegradelevel_key", IsUnique = true)]
    public partial class Gradelevel
    {
        public Gradelevel()
        {
            Blockleaders = new HashSet<Blockleader>();
            Classes = new HashSet<Class>();
            Exams = new HashSet<Exam>();
            Studentyearlystatuses = new HashSet<Studentyearlystatus>();
        }

        [Key]
        [Column("gradelevelid")]
        public Guid Gradelevelid { get; set; }
        [Column("gradelevelname")]
        [StringLength(100)]
        public string Gradelevelname { get; set; } = null!;
        [Column("codegradelevel")]
        [StringLength(20)]
        public string? Codegradelevel { get; set; }
        [Column("teacherid")]
        public Guid? Teacherid { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }
        [Column("updatedat")]
        public DateTime? Updatedat { get; set; }

        [ForeignKey("Teacherid")]
        [InverseProperty("Gradelevels")]
        public virtual Teacher? Teacher { get; set; }
        [InverseProperty("Gradelevel")]
        public virtual ICollection<Blockleader> Blockleaders { get; set; }
        [InverseProperty("Gradelevel")]
        public virtual ICollection<Class> Classes { get; set; }
        [InverseProperty("Gradelevel")]
        public virtual ICollection<Exam> Exams { get; set; }
        [InverseProperty("Gradelevel")]
        public virtual ICollection<Studentyearlystatus> Studentyearlystatuses { get; set; }
    }
}
