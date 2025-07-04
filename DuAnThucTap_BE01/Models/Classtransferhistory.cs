using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("classtransferhistory")]
    public partial class Classtransferhistory
    {
        [Key]
        [Column("transferid")]
        public Guid Transferid { get; set; }
        [Column("studentid")]
        public Guid Studentid { get; set; }
        [Column("fromclassid")]
        public Guid? Fromclassid { get; set; }
        [Column("toclassid")]
        public Guid Toclassid { get; set; }
        [Column("semesterid")]
        public Guid Semesterid { get; set; }
        [Column("transferdate")]
        public DateOnly Transferdate { get; set; }
        [Column("reason")]
        public string? Reason { get; set; }
        [Column("attachmenturl")]
        public string? Attachmenturl { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }

        [ForeignKey("Fromclassid")]
        [InverseProperty("ClasstransferhistoryFromclasses")]
        public virtual Class? Fromclass { get; set; }
        [ForeignKey("Semesterid")]
        [InverseProperty("Classtransferhistories")]
        public virtual Semester Semester { get; set; } = null!;
        [ForeignKey("Studentid")]
        [InverseProperty("Classtransferhistories")]
        public virtual Student Student { get; set; } = null!;
        [ForeignKey("Toclassid")]
        [InverseProperty("ClasstransferhistoryToclasses")]
        public virtual Class Toclass { get; set; } = null!;
    }
}
