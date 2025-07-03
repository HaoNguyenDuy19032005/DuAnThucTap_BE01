using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("topiclist")]
    [Index("Topicid", Name = "topiclist_topicid_key", IsUnique = true)]
    public partial class Topiclist
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("topicid")]
        [StringLength(255)]
        public string Topicid { get; set; } = null!;
        [Column("topicname")]
        [StringLength(255)]
        public string Topicname { get; set; } = null!;
        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; } = null!;
        [Column("teachingenddate")]
        public DateOnly Teachingenddate { get; set; }
        [Column("fk_subjectid")]
        [StringLength(255)]
        public string? FkSubjectid { get; set; }

        public virtual Subject? FkSubject { get; set; }
    }
}
