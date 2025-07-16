using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DuAnThucTap_BE01.Models
{
    [Table("examschedules")]
    [Index("examid", "classid", Name = "examschedules_examid_classid_key", IsUnique = true)]
    public partial class Examschedule
    {
        public Examschedule()
        {
            Examgraders = new HashSet<Examgrader>();
        }

        [Key]
        [Column("examscheduleid")]
        public int Examscheduleid { get; set; }

        [Required]
        [Column("examid")]
        [JsonProperty("examid")]
        public int examid { get; set; } // TÊN BIẾN PHẢI TRÙNG JSON

        [Required]
        [Column("classid")]
        [JsonProperty("classid")]
        public int classid { get; set; } // TÊN BIẾN PHẢI TRÙNG JSON

        [ForeignKey("classid")]
        [InverseProperty("Examschedules")]
        [JsonIgnore]
        public virtual Class Class { get; set; } = null!;

        [ForeignKey("examid")]
        [InverseProperty("Examschedules")]
        [JsonIgnore]
        public virtual Exam Exam { get; set; } = null!;

        [InverseProperty("Examschedule")]
        [JsonIgnore]
        public virtual ICollection<Examgrader> Examgraders { get; set; }
    }
}
