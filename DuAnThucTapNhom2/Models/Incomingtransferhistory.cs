using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("incomingtransferhistory")]
    [Index("Incomingtransferid", Name = "incomingtransferhistory_incomingtransferid_key", IsUnique = true)]
    public partial class Incomingtransferhistory
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("incomingtransferid")]
        [StringLength(255)]
        public string Incomingtransferid { get; set; } = null!;
        [Column("fk_studentid")]
        [StringLength(255)]
        public string FkStudentid { get; set; } = null!;
        [Column("arrivaldate")]
        public DateOnly Arrivaldate { get; set; }
        [Column("previousschoolname")]
        [StringLength(255)]
        public string Previousschoolname { get; set; } = null!;
        [Column("previousschoolprovince")]
        [StringLength(255)]
        public string Previousschoolprovince { get; set; } = null!;
        [Column("previousschooldistrict")]
        [StringLength(255)]
        public string Previousschooldistrict { get; set; } = null!;
        [Column("reason")]
        [StringLength(255)]
        public string Reason { get; set; } = null!;
        [Column("attachmenturl")]
        [StringLength(255)]
        public string? Attachmenturl { get; set; }
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime Createdat { get; set; }
        [Column("fk_semesterid")]
        [StringLength(255)]
        public string FkSemesterid { get; set; } = null!;

        public virtual Semester FkSemester { get; set; } = null!;
        public virtual Student FkStudent { get; set; } = null!;
    }
}
