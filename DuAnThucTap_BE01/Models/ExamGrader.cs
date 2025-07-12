using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace DuAnThucTap_BE01.Models
{
    [Table("examgraders")]
    [Index("Examscheduleid", "Teacherid", Name = "examgraders_examscheduleid_teacherid_key", IsUnique = true)]
    public partial class Examgrader
    {
        [Key]
        [Column("examgraderid")]
        public int Examgraderid { get; set; }

        // Made Examscheduleid nullable by using 'int?'
        [Column("examscheduleid")]
        public int? Examscheduleid { get; set; }

        // Made Teacherid nullable by using 'int?'
        [Column("teacherid")]
        public int? Teacherid { get; set; }

        [ForeignKey("Examscheduleid")]
        [InverseProperty("Examgraders")]
        [JsonIgnore]
        // Made navigation property nullable
        public virtual Examschedule? Examschedule { get; set; }

        [ForeignKey("Teacherid")]
        [InverseProperty("Examgraders")]
        [JsonIgnore]
        // Made navigation property nullable
        public virtual Teacher? Teacher { get; set; }
    }
}
