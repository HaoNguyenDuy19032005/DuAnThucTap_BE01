using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("operationunit")]
    [Index("Operationunitid", Name = "operationunit_operationunitid_key", IsUnique = true)]
    public partial class Operationunit
    {
        public Operationunit()
        {
            Teacherworkhistories = new HashSet<Teacherworkhistory>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("operationunitid")]
        [StringLength(255)]
        public string Operationunitid { get; set; } = null!;
        [Column("organizationname")]
        [StringLength(255)]
        public string Organizationname { get; set; } = null!;
        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; } = null!;
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime Createdat { get; set; }

        public virtual ICollection<Teacherworkhistory> Teacherworkhistories { get; set; }
    }
}
