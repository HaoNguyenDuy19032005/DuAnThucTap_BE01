using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTapNhom3.Models
{
    public partial class Schoolyear
    {
        public Schoolyear()
        {
            Semesters = new HashSet<Semester>();
        }
        [Key]
        public int Schoolyearid { get; set; }
        public string Schoolyearname { get; set; } = null!;
        public int Startyear { get; set; }
        public int Endyear { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        [JsonIgnore]
        public virtual ICollection<Semester>? Semesters { get; set; }
        public int SchoolYearlyStatusId { get; set; }
        [JsonIgnore]
        public virtual ICollection<SchoolYearlyStatus>? SchoolYearlyStatuses { get; set; }
    }
}
