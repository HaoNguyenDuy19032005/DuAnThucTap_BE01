using System.Diagnostics;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Semester
    {
        public int Semesterid { get; set; }
        public int Schoolyearid { get; set; }
        public string Semestername { get; set; } = null!;
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }

        public virtual Schoolyear Schoolyear { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
