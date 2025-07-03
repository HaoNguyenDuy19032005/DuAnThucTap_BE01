using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("teachertraininghistory")]
    [Index("Trainingid", Name = "teachertraininghistory_trainingid_key", IsUnique = true)]
    public partial class Teachertraininghistory
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("trainingid")]
        [StringLength(255)]
        public string Trainingid { get; set; } = null!;
        [Column("fk_teacherid")]
        [StringLength(255)]
        public string FkTeacherid { get; set; } = null!;
        [Column("traininginstitutionname")]
        [StringLength(255)]
        public string Traininginstitutionname { get; set; } = null!;
        [Column("majororspecialization")]
        [StringLength(255)]
        public string Majororspecialization { get; set; } = null!;
        [Column("startdate")]
        public DateOnly Startdate { get; set; }
        [Column("enddateorgraduationyear")]
        public DateOnly Enddateorgraduationyear { get; set; }
        [Required]
        [Column("active")]
        public bool? Active { get; set; }
        [Column("trainingtype")]
        [StringLength(255)]
        public string Trainingtype { get; set; } = null!;
        [Column("certificatediplomaname")]
        [StringLength(255)]
        public string Certificatediplomaname { get; set; } = null!;
        [Column("decisionfileurl")]
        [StringLength(255)]
        public string Decisionfileurl { get; set; } = null!;

        public virtual Teacher FkTeacher { get; set; } = null!;
    }
}
