using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Nhom2ThucTap.Models
{
    [Table("testassignments")]
    public partial class Testassignment
    {
        //public Testassignment()
        //{
        //    //Studenttestsubmissions = new HashSet<Studenttestsubmission>();
        //}

        [Key]
        [Column("assignmentid")]
        public int Assignmentid { get; set; }
        [Column("testid")]
        public int Testid { get; set; }
        [Column("classid")]
        public int Classid { get; set; }
        [Column("status")]
        public string? Status { get; set; }

        [ForeignKey("Classid")]
        [InverseProperty("Testassignments")]
        public virtual Class Class { get; set; } = null!;
        [ForeignKey("Testid")]
        [InverseProperty("Testassignments")]
        public virtual Test Test { get; set; } = null!;
        //[InverseProperty("Assignment")]
        //public virtual ICollection<Studenttestsubmission> Studenttestsubmissions { get; set; }
    }


}
