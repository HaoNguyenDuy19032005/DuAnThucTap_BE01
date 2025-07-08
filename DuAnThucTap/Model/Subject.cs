using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Subject
    {
        [Key]
        public int Subjectid { get; set; }
        public string Subjectname { get; set; } = null!;
        public string? Subjectcode { get; set; }
        public int? Defaultperiodssem1 { get; set; }
        public int? Defaultperiodssem2 { get; set; }
        public int? Departmentid { get; set; }
        public int? Subjecttypeid { get; set; }
        public int? Schoolyearid { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }

        [JsonIgnore]
        public virtual Department? Department { get; set; }

        [JsonIgnore]
        public virtual Schoolyear? Schoolyear { get; set; }

        [JsonIgnore]
        public virtual Subjecttype? Subjecttype { get; set; }

        [JsonIgnore]
        public virtual ICollection<Class>? Classes { get; set; }

        [JsonIgnore]
        public virtual ICollection<Grade>? Grades { get; set; }

        [JsonIgnore]
        public virtual ICollection<Teacher>? Teachers { get; set; }

        [JsonIgnore]
        public virtual ICollection<Teachingassignment>? Teachingassignments { get; set; }
    }
}
