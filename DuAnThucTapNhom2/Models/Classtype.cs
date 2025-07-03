using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("classtypes")]
    [Index("Classtypeid", Name = "classtypes_classtypeid_key", IsUnique = true)]
    public partial class Classtype
    {
        public Classtype()
        {
            Classes = new HashSet<Class>();
            Teachingassignments = new HashSet<Teachingassignment>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("classtypeid")]
        [StringLength(255)]
        public string Classtypeid { get; set; } = null!;
        [Column("classname")]
        [StringLength(255)]
        public string Classname { get; set; } = null!;
        [Column("maxstudents")]
        public int Maxstudents { get; set; }
        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; } = null!;
        [Required]
        [Column("isactive")]
        public bool? Isactive { get; set; }
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime Createdat { get; set; }
        [Column("updatedat", TypeName = "timestamp without time zone")]
        public DateTime Updatedat { get; set; }
        [Column("fk_schoolyearid")]
        [StringLength(255)]
        public string FkSchoolyearid { get; set; } = null!;
        [Column("fk_gradelevelid")]
        [StringLength(255)]
        public string FkGradelevelid { get; set; } = null!;
        [Column("fk_homeroomteacherid")]
        [StringLength(255)]
        public string FkHomeroomteacherid { get; set; } = null!;
        [Column("fk_subjectid")]
        [StringLength(255)]
        public string FkSubjectid { get; set; } = null!;

        public virtual Gradelevel FkGradelevel { get; set; } = null!;
        public virtual Teacher FkHomeroomteacher { get; set; } = null!;
        public virtual Schoolyear FkSchoolyear { get; set; } = null!;
        public virtual Subject FkSubject { get; set; } = null!;
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Teachingassignment> Teachingassignments { get; set; }
    }
}
