using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTapNhom3.Models
{
    public partial class Class
    {
        [Key]
        public int Classid { get; set; }
        public string Originalfilename { get; set; } = null!;
        public string Storedfilepath { get; set; } = null!;
        public string Classname { get; set; } = null!;
        public int? Maxstudents { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        public int? Schoolyearid { get; set; }
        public int? Gradelevelid { get; set; }
        public int? Classtypeid { get; set; }
        public int? Homeroomteacherid { get; set; }
        public int? Subjectid { get; set; }
        [JsonIgnore]
        public virtual Classtype? Classtype { get; set; }
        [JsonIgnore]
        public virtual Gradelevel? Gradelevel { get; set; }
        [JsonIgnore]
        public virtual Subject? Subject { get; set; }
        [JsonIgnore]
        public ICollection<Student>? Students { get; set; }
        [JsonIgnore]
        public virtual ICollection<StudentSemesterSummary>? StudentSemesterSummaries { get; set; }
        public int SchoolYearlyStatusId { get; set; }
        [JsonIgnore]
        public virtual ICollection<SchoolYearlyStatus>? SchoolYearlyStatuses { get; set; }
    }
}
