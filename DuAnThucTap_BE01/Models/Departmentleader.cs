using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("departmentleaders")]
    public partial class Departmentleader
    {
        [Key]
        [Column("departmentleaderid")]
        public Guid Departmentleaderid { get; set; }
        [Column("departmentid")]
        public Guid Departmentid { get; set; }
        [Column("schoolyearid")]
        public Guid Schoolyearid { get; set; }
        [Column("teacherid")]
        public Guid Teacherid { get; set; }
        [Column("startdate")]
        public DateOnly? Startdate { get; set; }
        [Column("enddate")]
        public DateOnly? Enddate { get; set; }

        [ForeignKey("Departmentid")]
        [InverseProperty("Departmentleaders")]
        public virtual Department Department { get; set; } = null!;
        [ForeignKey("Schoolyearid")]
        [InverseProperty("Departmentleaders")]
        public virtual Schoolyear Schoolyear { get; set; } = null!;
        [ForeignKey("Teacherid")]
        [InverseProperty("Departmentleaders")]
        public virtual Teacher Teacher { get; set; } = null!;
    }
}
