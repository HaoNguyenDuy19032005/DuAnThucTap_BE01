using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("blockleaders")]
    public partial class Blockleader
    {
        [Key]
        [Column("blockleaderid")]
        public int Blockleaderid { get; set; }
        [Column("gradelevelid")]
        public int Gradelevelid { get; set; }
        [Column("schoolyearid")]
        public int Schoolyearid { get; set; }
        [Column("teacherid")]
        public int Teacherid { get; set; }
        [Column("startdate")]
        public DateOnly? Startdate { get; set; }
        [Column("enddate")]
        public DateOnly? Enddate { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }
        [Column("updatedat")]
        public DateTime? Updatedat { get; set; }

        [ForeignKey("Gradelevelid")]
        [InverseProperty("Blockleaders")]
        public virtual Gradelevel Gradelevel { get; set; } = null!;
        [ForeignKey("Schoolyearid")]
        [InverseProperty("Blockleaders")]
        public virtual Schoolyear Schoolyear { get; set; } = null!;
        [ForeignKey("Teacherid")]
        [InverseProperty("Blockleaders")]
        public virtual Teacher Teacher { get; set; } = null!;
    }
}
