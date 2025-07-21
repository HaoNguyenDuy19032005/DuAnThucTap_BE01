using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhom2ThucTap.Models
{
    [Table("tests")]
    public partial class Test
    {
        public Test()
        {
            Testassignments = new HashSet<Testassignment>();
            //Testquestions = new HashSet<Testquestion>();
        }

        [Key]
        [Column("testid")]
        public int Testid { get; set; }
        [Column("teacherid")]
        public int Teacherid { get; set; }
        [Column("title")]
        public string Title { get; set; } = null!;
        [Column("testformat")]
        public string? Testformat { get; set; }
        [Column("durationinminutes")]
        public int? Durationinminutes { get; set; }
        [Column("starttime", TypeName = "timestamp without time zone")]
        public DateTime? Starttime { get; set; }
        [Column("endtime", TypeName = "timestamp without time zone")]
        public DateTime? Endtime { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("classification")]
        public string? Classification { get; set; }
        [Column("attachmenturl")]
        public string? Attachmenturl { get; set; }
        [Column("requirestudentattachment")]
        public bool? Requirestudentattachment { get; set; }

        [ForeignKey("Teacherid")]
        [InverseProperty("Tests")]
        public virtual Teacher Teacher { get; set; } = null!;
        [InverseProperty("Test")]
        public virtual ICollection<Testassignment> Testassignments { get; set; }
        //[InverseProperty("Test")]
        //public virtual ICollection<Testquestion> Testquestions { get; set; }
    }
}
