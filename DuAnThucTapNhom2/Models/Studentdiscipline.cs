using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("studentdisciplines")]
    [Index("Disciplineid", Name = "studentdisciplines_disciplineid_key", IsUnique = true)]
    public partial class Studentdiscipline
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("disciplineid")]
        [StringLength(255)]
        public string Disciplineid { get; set; } = null!;
        [Column("fk_studentid")]
        [StringLength(255)]
        public string FkStudentid { get; set; } = null!;

        public virtual Student FkStudent { get; set; } = null!;
    }
}
