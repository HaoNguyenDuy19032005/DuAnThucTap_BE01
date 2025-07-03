using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("testassignments")]
    [Index("Assignmentid", Name = "testassignments_assignmentid_key", IsUnique = true)]
    public partial class Testassignment
    {
        public Testassignment()
        {
            Studenttestsubmissions = new HashSet<Studenttestsubmission>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("assignmentid")]
        [StringLength(255)]
        public string Assignmentid { get; set; } = null!;
        [Column("fk_testid")]
        [StringLength(255)]
        public string FkTestid { get; set; } = null!;
        [Column("fk_classid")]
        [StringLength(255)]
        public string FkClassid { get; set; } = null!;
        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; } = null!;
        [Column("status")]
        [StringLength(255)]
        public string Status { get; set; } = null!;
        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; } = null!;
        [Column("sentat", TypeName = "timestamp without time zone")]
        public DateTime Sentat { get; set; }
        [Column("attachmenturl")]
        [StringLength(255)]
        public string Attachmenturl { get; set; } = null!;
        [Column("displayorder")]
        public int Displayorder { get; set; }
        [Column("optiona")]
        [StringLength(255)]
        public string Optiona { get; set; } = null!;
        [Column("optionb")]
        [StringLength(255)]
        public string Optionb { get; set; } = null!;
        [Column("optionc")]
        [StringLength(255)]
        public string Optionc { get; set; } = null!;
        [Column("optiond")]
        [StringLength(255)]
        public string Optiond { get; set; } = null!;
        [Column("correctoption")]
        [StringLength(255)]
        public string Correctoption { get; set; } = null!;
        [Column("points")]
        public double Points { get; set; }

        public virtual Class FkClass { get; set; } = null!;
        public virtual Test FkTest { get; set; } = null!;
        public virtual ICollection<Studenttestsubmission> Studenttestsubmissions { get; set; }
    }
}
