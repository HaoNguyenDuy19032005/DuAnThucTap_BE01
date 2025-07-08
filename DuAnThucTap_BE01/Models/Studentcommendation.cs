using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("studentcommendations")]
    public partial class Studentcommendation
    {
        [Key]
        [Column("commendationid")]
        public int Commendationid { get; set; }
        [Column("studentid")]
        public int Studentid { get; set; }
        [Column("semesterid")]
        public int Semesterid { get; set; }
        [Column("schoolid")]
        public int Schoolid { get; set; }
        [Column("commendationtypeid")]
        public int Commendationtypeid { get; set; }
        [Column("commendationdate")]
        public DateOnly Commendationdate { get; set; }
        [Column("content")]
        public string? Content { get; set; }
        [Column("attachmenturl")]
        public string? Attachmenturl { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }

        [ForeignKey("Commendationtypeid")]
        [InverseProperty("Studentcommendations")]
        public virtual Commendationtype Commendationtype { get; set; } = null!;
        [ForeignKey("Schoolid")]
        [InverseProperty("Studentcommendations")]
        public virtual Schoolinformation School { get; set; } = null!;
        [ForeignKey("Semesterid")]
        [InverseProperty("Studentcommendations")]
        public virtual Semester Semester { get; set; } = null!;
        [ForeignKey("Studentid")]
        [InverseProperty("Studentcommendations")]
        public virtual Student Student { get; set; } = null!;
    }
}
