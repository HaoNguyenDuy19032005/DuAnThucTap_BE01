using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("studentdisciplines")]
    public partial class Studentdiscipline
    {
        [Key]
        [Column("disciplineid")]
        public Guid Disciplineid { get; set; }
        [Column("studentid")]
        public Guid Studentid { get; set; }
        [Column("semesterid")]
        public Guid Semesterid { get; set; }
        [Column("schoolid")]
        public Guid Schoolid { get; set; }
        [Column("disciplinetypeid")]
        public Guid Disciplinetypeid { get; set; }
        [Column("disciplinedate")]
        public DateOnly Disciplinedate { get; set; }
        [Column("content")]
        public string? Content { get; set; }
        [Column("attachmenturl")]
        public string? Attachmenturl { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }

        [ForeignKey("Disciplinetypeid")]
        [InverseProperty("Studentdisciplines")]
        public virtual Disciplinetype Disciplinetype { get; set; } = null!;
        [ForeignKey("Schoolid")]
        [InverseProperty("Studentdisciplines")]
        public virtual Schoolinformation School { get; set; } = null!;
        [ForeignKey("Semesterid")]
        [InverseProperty("Studentdisciplines")]
        public virtual Semester Semester { get; set; } = null!;
        [ForeignKey("Studentid")]
        [InverseProperty("Studentdisciplines")]
        public virtual Student Student { get; set; } = null!;
    }
}
