using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DuAnThucTap_BE01.Models
{
    public partial class Gradelevel
    {
        public Gradelevel()
        {
            Classes = new HashSet<Class>();
        }

        public int Gradelevelid { get; set; }
        public string GradeLevelName { get; set; } = null!;
        public string? Codegradelevel { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        public int? Headteacherid { get; set; }

        [JsonIgnore]
        public virtual ICollection<Class> Classes { get; set; }
    }
}
