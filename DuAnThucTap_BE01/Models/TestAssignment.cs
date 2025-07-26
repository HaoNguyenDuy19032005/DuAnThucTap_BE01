using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("testassignments")]
    [Index("Testid", "Classid", Name = "testassignments_testid_classid_key", IsUnique = true)]
    public partial class Testassignment
    {
        public Testassignment()
        {
            Studenttestsubmissions = new HashSet<Studenttestsubmission>();
        }

        [Key]
        [Column("assignmentid")]
        public int Assignmentid { get; set; }
        [Column("testid")]
        public int Testid { get; set; }
        [Column("classid")]
        public int Classid { get; set; }
        [Column("status")]
        [StringLength(50)]
        public string? Status { get; set; }

        [ForeignKey("Classid")]
        [InverseProperty("Testassignments")]
        public virtual Class Class { get; set; } = null!;
        [ForeignKey("Testid")]
        [InverseProperty("Testassignments")]
        public virtual Test Test { get; set; } = null!;
        [InverseProperty("Assignment")]
        public virtual ICollection<Studenttestsubmission> Studenttestsubmissions { get; set; }
    }
}
