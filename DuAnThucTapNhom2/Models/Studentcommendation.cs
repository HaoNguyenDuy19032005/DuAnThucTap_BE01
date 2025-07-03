using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("studentcommendations")]
    [Index("Commendationid", Name = "studentcommendations_commendationid_key", IsUnique = true)]
    public partial class Studentcommendation
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("commendationid")]
        [StringLength(255)]
        public string Commendationid { get; set; } = null!;
        [Column("fk_studentid")]
        [StringLength(255)]
        public string FkStudentid { get; set; } = null!;
        [Column("commendationdate")]
        public DateOnly Commendationdate { get; set; }
        [Column("content")]
        public string? Content { get; set; }
        [Column("attachmenturl")]
        [StringLength(255)]
        public string? Attachmenturl { get; set; }
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime Createdat { get; set; }

        public virtual Student FkStudent { get; set; } = null!;
    }
}
