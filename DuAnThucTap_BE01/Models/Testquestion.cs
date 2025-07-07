using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("testquestions")]
    public partial class Testquestion
    {
        public Testquestion()
        {
            Studenttestanswers = new HashSet<Studenttestanswer>();
        }

        [Key]
        [Column("questionid")]
        public Guid Questionid { get; set; }
        [Column("testid")]
        public Guid Testid { get; set; }
        [Column("title")]
        public string? Title { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("attachmenturl")]
        public string? Attachmenturl { get; set; }
        [Column("displayorder")]
        public int? Displayorder { get; set; }
        [Column("optiona")]
        public string? Optiona { get; set; }
        [Column("optionb")]
        public string? Optionb { get; set; }
        [Column("optionc")]
        public string? Optionc { get; set; }
        [Column("optiond")]
        public string? Optiond { get; set; }
        [Column("correctoption")]
        [StringLength(10)]
        public string? Correctoption { get; set; }
        [Column("points")]
        [Precision(5, 2)]
        public decimal? Points { get; set; }

        [ForeignKey("Testid")]
        [InverseProperty("Testquestions")]
        public virtual Test Test { get; set; } = null!;
        [InverseProperty("Question")]
        public virtual ICollection<Studenttestanswer> Studenttestanswers { get; set; }
    }
}
