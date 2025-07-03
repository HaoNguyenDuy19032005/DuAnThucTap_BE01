using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("gradelevels")]
    [Index("Gradelevelid", Name = "gradelevels_gradelevelid_key", IsUnique = true)]
    public partial class Gradelevel
    {
        public Gradelevel()
        {
            Classes = new HashSet<Class>();
            Classtypes = new HashSet<Classtype>();
            Exams = new HashSet<Exam>();
            Students = new HashSet<Student>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("gradelevelid")]
        [StringLength(255)]
        public string Gradelevelid { get; set; } = null!;
        [Column("gradelevelname")]
        [StringLength(255)]
        public string Gradelevelname { get; set; } = null!;
        [Column("codegradelevel")]
        [StringLength(255)]
        public string Codegradelevel { get; set; } = null!;
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime Createdat { get; set; }
        [Column("updatedat", TypeName = "timestamp without time zone")]
        public DateTime Updatedat { get; set; }
        [Column("fk_teacherid")]
        [StringLength(255)]
        public string FkTeacherid { get; set; } = null!;

        public virtual Teacher FkTeacher { get; set; } = null!;
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Classtype> Classtypes { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
