using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("tests")]
    public partial class Test
    {
        public Test()
        {
            Testassignments = new HashSet<Testassignment>();
            Testquestions = new HashSet<Testquestion>();
        }

        [Key]
        [Column("testid")]
        public int Testid { get; set; }

        [Column("teacherid")]
        public int Teacherid { get; set; }

        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; } = null!;

        [Column("testformat")]
        [StringLength(100)]
        public string? Testformat { get; set; }

        [Column("durationinminutes")]
        public int? Durationinminutes { get; set; }

        [Column("starttime")]
        public DateTime? Starttime { get; set; }

        [Column("endtime")]
        public DateTime? Endtime { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("classification")]
        [StringLength(100)]
        public string? Classification { get; set; }

        [Column("attachmenturl")]
        public string? Attachmenturl { get; set; }

        [Column("requirestudentattachment")]
        public bool Requirestudentattachment { get; set; } = false;

        [ForeignKey("Teacherid")]
        [InverseProperty("Tests")]
        [JsonIgnore]
        public virtual Teacher? Teacher { get; set; }

        [InverseProperty("Test")]
        [JsonIgnore]
        public virtual ICollection<Testassignment> Testassignments { get; set; }

        [InverseProperty("Test")]
        [JsonIgnore]
        public virtual ICollection<Testquestion> Testquestions { get; set; }
    }
}