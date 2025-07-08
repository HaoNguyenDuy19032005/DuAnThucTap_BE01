using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTapNhom3.Models
{
    public partial class Semester
    {
        [Key]
        public int Semesterid { get; set; }
        public int Schoolyearid { get; set; }
        public string Semestername { get; set; } = null!;
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        [JsonIgnore]
        public virtual Schoolyear? Schoolyear { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<StudentSemesterSummary> StudentSemesterSummaries { get; set; } = new HashSet<StudentSemesterSummary>();
        public virtual ICollection<SchoolYearlyStatus>? SchoolYearlyStatuses { get; set; }
    }
}
