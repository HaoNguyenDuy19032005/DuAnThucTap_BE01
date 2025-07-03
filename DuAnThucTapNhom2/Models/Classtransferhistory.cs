using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("classtransferhistory")]
    [Index("Transferid", Name = "classtransferhistory_transferid_key", IsUnique = true)]
    public partial class Classtransferhistory
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("transferid")]
        [StringLength(255)]
        public string Transferid { get; set; } = null!;
        [Column("transferdate")]
        public DateOnly Transferdate { get; set; }
        [Column("reason")]
        [StringLength(255)]
        public string Reason { get; set; } = null!;
        [Column("attachmenturl")]
        [StringLength(255)]
        public string? Attachmenturl { get; set; }
        [Column("fk_studentid")]
        [StringLength(255)]
        public string FkStudentid { get; set; } = null!;
        [Column("fromclassid")]
        [StringLength(255)]
        public string Fromclassid { get; set; } = null!;
        [Column("fk_toclassid")]
        [StringLength(255)]
        public string FkToclassid { get; set; } = null!;
        [Column("fk_semesterid")]
        [StringLength(255)]
        public string FkSemesterid { get; set; } = null!;
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime Createdat { get; set; }

        public virtual Semester FkSemester { get; set; } = null!;
        public virtual Student FkStudent { get; set; } = null!;
        public virtual Class FkToclass { get; set; } = null!;
        public virtual Class Fromclass { get; set; } = null!;
    }
}
