using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("schooltransferhistory")]
    [Index("Schooltransferid", Name = "schooltransferhistory_schooltransferid_key", IsUnique = true)]
    public partial class Schooltransferhistory
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("schooltransferid")]
        [StringLength(255)]
        public string Schooltransferid { get; set; } = null!;
        [Column("fromclassid")]
        [StringLength(255)]
        public string Fromclassid { get; set; } = null!;
        [Column("toschoolname")]
        [StringLength(255)]
        public string Toschoolname { get; set; } = null!;
        [Column("toschooladdress")]
        [StringLength(255)]
        public string Toschooladdress { get; set; } = null!;
        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; } = null!;
        [Required]
        [Column("isactive")]
        public bool? Isactive { get; set; }
        [Column("fk_studentid")]
        [StringLength(255)]
        public string FkStudentid { get; set; } = null!;
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime Createdat { get; set; }

        public virtual Student FkStudent { get; set; } = null!;
        public virtual Class Fromclass { get; set; } = null!;
    }
}
