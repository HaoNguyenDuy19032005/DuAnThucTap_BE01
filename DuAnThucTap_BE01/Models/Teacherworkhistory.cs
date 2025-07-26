using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace DuAnThucTap_BE01.Models
{
    [Table("teacherworkhistory")]
    // Đã bỏ IValidatableObject
    public partial class Teacherworkhistory
    {
        [Key]
        [Column("workhistoryid")]
        public int Workhistoryid { get; set; }

        [Column("teacherid")]
        public int Teacherid { get; set; }

        [Column("operationunitid")]
        public int? Operationunitid { get; set; }

        [Column("departmentid")]
        public int? Departmentid { get; set; }

        [Column("iscurrentschool")]
        public bool? Iscurrentschool { get; set; }

        [Column("positionheld")]
        [StringLength(150)] // Giữ lại StringLength để khớp với database
        public string? Positionheld { get; set; }

        [Column("startdate")]
        public DateOnly? Startdate { get; set; }

        [Column("enddate")]
        public DateOnly? Enddate { get; set; }

        [ForeignKey("Departmentid")]
        [InverseProperty("Teacherworkhistories")]
        [JsonIgnore]
        public virtual Department? Department { get; set; }

        [ForeignKey("Operationunitid")]
        [InverseProperty("Teacherworkhistories")]
        [JsonIgnore]
        public virtual Operationunit? Operationunit { get; set; }

        [ForeignKey("Teacherid")]
        [InverseProperty("Teacherworkhistories")]
        [JsonIgnore]
        public virtual Teacher? Teacher { get; set; }

        // Đã xóa toàn bộ phương thức Validate()
    }
}