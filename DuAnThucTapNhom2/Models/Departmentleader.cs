using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("departmentleaders")]
    [Index("Departmentleaderid", Name = "departmentleaders_departmentleaderid_key", IsUnique = true)]
    public partial class Departmentleader
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("departmentleaderid")]
        [StringLength(255)]
        public string Departmentleaderid { get; set; } = null!;
        [Column("fk_departmentid")]
        [StringLength(255)]
        public string FkDepartmentid { get; set; } = null!;
        [Column("fk_schoolyearid")]
        [StringLength(255)]
        public string FkSchoolyearid { get; set; } = null!;
        [Column("fk_teacherid")]
        [StringLength(255)]
        public string FkTeacherid { get; set; } = null!;
        [Column("startdate")]
        public DateOnly Startdate { get; set; }
        [Column("enddate")]
        public DateOnly Enddate { get; set; }

        public virtual Department FkDepartment { get; set; } = null!;
        public virtual Schoolyear FkSchoolyear { get; set; } = null!;
        public virtual Teacher FkTeacher { get; set; } = null!;
    }
}
