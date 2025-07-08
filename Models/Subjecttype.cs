using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTapNhom3.Models
{
    public partial class Subjecttype
    {
        public Subjecttype()
        {
            Subjects = new HashSet<Subject>();
        }
        [Key]
        public int Subjecttypeid { get; set; }
        public string Subjecttypename { get; set; } = null!;
        public string? Description { get; set; }
        public bool? Isactive { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        [JsonIgnore]
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
