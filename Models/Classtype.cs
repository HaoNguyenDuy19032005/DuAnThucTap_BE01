using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTapNhom3.Models
{
    public partial class Classtype
    {
        public Classtype()
        {
            Classes = new HashSet<Class>();
        }
        [Key]
        public int Classtypeid { get; set; }
        public string Classtypename { get; set; } = null!;
        public string? Description { get; set; }
        public bool? Isactive { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        [JsonIgnore]
        public virtual ICollection<Class> Classes { get; set; }
    }
}
