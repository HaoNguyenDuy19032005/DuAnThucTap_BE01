using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTapNhom3.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Classes = new HashSet<Class>();
        }
        [Key]
        public int Subjectid { get; set; }
        public string Subjectname { get; set; } = null!;
        public string? Subjectcode { get; set; }
        public int? Defaultperiodssem1 { get; set; }
        public int? Defaultperiodssem2 { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }

        public int? Departmentid { get; set; }
        public int? Subjecttypeid { get; set; }

        [JsonIgnore]
        public virtual Department? Department { get; set; }
        [JsonIgnore]
        public virtual Subjecttype? Subjecttype { get; set; }
        [JsonIgnore]
        public virtual ICollection<Class> Classes { get; set; }
    }
}
