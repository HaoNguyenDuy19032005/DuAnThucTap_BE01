using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTapNhom3.Models
{
    public partial class Gradelevel
    {
        public Gradelevel()
        {
            Classes = new HashSet<Class>();
        }
        [Key]
        public int Gradelevelid { get; set; }
        public string Gradelevelname { get; set; } = null!;
        public string? Codegradelevel { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        public int? Headteacherid { get; set; }
        [JsonIgnore]
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<SchoolYearlyStatus>? SchoolYearlyStatuses { get; set; }

    }
}
