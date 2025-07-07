using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("schooltransferhistory")]
    public partial class Schooltransferhistory
    {
        [Key]
        [Column("schooltransferid")]
        public Guid Schooltransferid { get; set; }
        [Column("studentid")]
        public Guid Studentid { get; set; }
        [Column("fromschoolid")]
        public Guid? Fromschoolid { get; set; }
        [Column("fromclassid")]
        public Guid? Fromclassid { get; set; }
        [Column("toschoolid")]
        public Guid Toschoolid { get; set; }
        [Column("toclassid")]
        public Guid Toclassid { get; set; }
        [Column("transfertype")]
        [StringLength(100)]
        public string? Transfertype { get; set; }
        [Column("transferdate")]
        public DateOnly Transferdate { get; set; }
        [Column("reason")]
        public string? Reason { get; set; }
        [Column("attachmenturl")]
        public string? Attachmenturl { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }

        [ForeignKey("Fromclassid")]
        [InverseProperty("SchooltransferhistoryFromclasses")]
        public virtual Class? Fromclass { get; set; }
        [ForeignKey("Fromschoolid")]
        [InverseProperty("SchooltransferhistoryFromschools")]
        public virtual Schoolinformation? Fromschool { get; set; }
        [ForeignKey("Studentid")]
        [InverseProperty("Schooltransferhistories")]
        public virtual Student Student { get; set; } = null!;
        [ForeignKey("Toclassid")]
        [InverseProperty("SchooltransferhistoryToclasses")]
        public virtual Class Toclass { get; set; } = null!;
        [ForeignKey("Toschoolid")]
        [InverseProperty("SchooltransferhistoryToschools")]
        public virtual Schoolinformation Toschool { get; set; } = null!;
    }
}
