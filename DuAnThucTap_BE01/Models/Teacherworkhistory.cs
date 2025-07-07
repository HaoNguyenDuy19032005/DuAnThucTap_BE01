using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("teacherworkhistory")]
    public partial class Teacherworkhistory
    {
        [Key]
        [Column("workhistoryid")]
        public Guid Workhistoryid { get; set; }
        [Column("teacherid")]
        public Guid Teacherid { get; set; }
        [Column("operationunitid")]
        public Guid? Operationunitid { get; set; }
        [Column("departmentid")]
        public Guid? Departmentid { get; set; }
        [Column("iscurrentschool")]
        public bool? Iscurrentschool { get; set; }
        [Column("positionheld")]
        [StringLength(150)]
        public string? Positionheld { get; set; }
        [Column("startdate")]
        public DateOnly? Startdate { get; set; }
        [Column("enddate")]
        public DateOnly? Enddate { get; set; }

        [ForeignKey("Departmentid")]
        [InverseProperty("Teacherworkhistories")]
        public virtual Department? Department { get; set; }
        [ForeignKey("Operationunitid")]
        [InverseProperty("Teacherworkhistories")]
        public virtual Operationunit? Operationunit { get; set; }
        [ForeignKey("Teacherid")]
        [InverseProperty("Teacherworkhistories")]
        public virtual Teacher Teacher { get; set; } = null!;
    }
}
