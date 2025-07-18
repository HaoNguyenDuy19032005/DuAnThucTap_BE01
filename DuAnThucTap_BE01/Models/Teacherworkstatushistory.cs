using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace DuAnThucTap_BE01.Models
{
    [Table("teacherworkstatushistory")]
    // Đã bỏ IValidatableObject
    public partial class Teacherworkstatushistory
    {
        [Key]
        [Column("historyid")]
        public int Historyid { get; set; }

        [Column("teacherid")]
        public int Teacherid { get; set; }

        [Column("statustype")]
        [StringLength(100)] // Giữ lại StringLength để khớp với database
        public string Statustype { get; set; } = null!;

        [Column("startdate")]
        public DateOnly? Startdate { get; set; }

        [Column("enddate")]
        public DateOnly? Enddate { get; set; }

        [Column("note")]
        public string? Note { get; set; }

        [Column("decisionfileurl")]
        public string? Decisionfileurl { get; set; }

        [Column("createdat")]
        public DateTime? Createdat { get; set; }

        [ForeignKey("Teacherid")]
        [InverseProperty("Teacherworkstatushistories")]
        [JsonIgnore]
        public virtual Teacher? Teacher { get; set; }

    }
}