using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("teacherworkhistory")]
    [Index("Workhistoryid", Name = "teacherworkhistory_workhistoryid_key", IsUnique = true)]
    public partial class Teacherworkhistory
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("workhistoryid")]
        [StringLength(255)]
        public string Workhistoryid { get; set; } = null!;
        [Column("organizationname")]
        [StringLength(255)]
        public string Organizationname { get; set; } = null!;
        [Column("iscurrentschool")]
        public bool Iscurrentschool { get; set; }
        [Column("positionheld")]
        [StringLength(255)]
        public string Positionheld { get; set; } = null!;
        [Column("startdate")]
        public DateOnly Startdate { get; set; }
        [Column("enddate")]
        public DateOnly Enddate { get; set; }
        [Column("fk_departmentid")]
        [StringLength(255)]
        public string FkDepartmentid { get; set; } = null!;
        [Column("fk_operationunitid")]
        [StringLength(255)]
        public string FkOperationunitid { get; set; } = null!;
        [Column("fk_teacherid")]
        [StringLength(255)]
        public string FkTeacherid { get; set; } = null!;

        public virtual Department FkDepartment { get; set; } = null!;
        public virtual Operationunit FkOperationunit { get; set; } = null!;
        public virtual Teacher FkTeacher { get; set; } = null!;
    }
}
