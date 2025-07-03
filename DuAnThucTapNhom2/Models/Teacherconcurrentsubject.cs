using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("teacherconcurrentsubjects")]
    [Index("FkTeacherid", "FkSubjectid", "FkSchoolyearid", Name = "teacherconcurrentsubjects_fk_teacherid_fk_subjectid_fk_scho_key", IsUnique = true)]
    public partial class Teacherconcurrentsubject
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("fk_teacherid")]
        [StringLength(255)]
        public string FkTeacherid { get; set; } = null!;
        [Column("fk_subjectid")]
        [StringLength(255)]
        public string FkSubjectid { get; set; } = null!;
        [Column("fk_schoolyearid")]
        [StringLength(255)]
        public string FkSchoolyearid { get; set; } = null!;

        public virtual Schoolyear FkSchoolyear { get; set; } = null!;
        public virtual Subject FkSubject { get; set; } = null!;
        public virtual Teacher FkTeacher { get; set; } = null!;
    }
}
