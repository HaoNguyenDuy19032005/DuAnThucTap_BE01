using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("tests")]
    [Index("Testid", Name = "tests_testid_key", IsUnique = true)]
    public partial class Test
    {
        public Test()
        {
            Testassignments = new HashSet<Testassignment>();
            Testquestions = new HashSet<Testquestion>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("testid")]
        [StringLength(255)]
        public string Testid { get; set; } = null!;
        [Column("fk_teacherid")]
        [StringLength(255)]
        public string? FkTeacherid { get; set; }
        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; } = null!;
        [Column("testformat")]
        [StringLength(255)]
        public string Testformat { get; set; } = null!;
        [Column("durationinminutes")]
        public int Durationinminutes { get; set; }
        [Column("starttime", TypeName = "timestamp without time zone")]
        public DateTime Starttime { get; set; }
        [Column("endtime", TypeName = "timestamp without time zone")]
        public DateTime Endtime { get; set; }
        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; } = null!;
        [Column("classification")]
        [StringLength(255)]
        public string Classification { get; set; } = null!;
        [Column("attachmenturl")]
        [StringLength(255)]
        public string Attachmenturl { get; set; } = null!;
        [Column("status")]
        [StringLength(255)]
        public string Status { get; set; } = null!;
        [Column("allowedfiletypes")]
        [StringLength(255)]
        public string Allowedfiletypes { get; set; } = null!;
        [Column("maxfilesizemb")]
        public double Maxfilesizemb { get; set; }

        public virtual Teacher? FkTeacher { get; set; }
        public virtual ICollection<Testassignment> Testassignments { get; set; }
        public virtual ICollection<Testquestion> Testquestions { get; set; }
    }
}
