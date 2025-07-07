using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("operationunit")]
    public partial class Operationunit
    {
        public Operationunit()
        {
            Teacherworkhistories = new HashSet<Teacherworkhistory>();
        }

        [Key]
        [Column("operationunitid")]
        public Guid Operationunitid { get; set; }
        [Column("organizationname")]
        [StringLength(255)]
        public string? Organizationname { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }

        [InverseProperty("Operationunit")]
        public virtual ICollection<Teacherworkhistory> Teacherworkhistories { get; set; }
    }
}
