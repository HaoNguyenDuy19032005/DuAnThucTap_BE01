using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Schoolyear
    {
        [Key]
        public int Schoolyearid { get; set; }
        public int Schoolinfoid { get; set; }
        public string Schoolyearname { get; set; } = null!;
        public int Startyear { get; set; }
        public int Endyear { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        [JsonIgnore]
        public virtual ICollection<Class>? Classes { get; set; }
        [JsonIgnore]
        public virtual ICollection<Semester>? Semesters { get; set; }
        [JsonIgnore]
        public virtual ICollection<Subject>? Subjects { get; set; }
        [JsonIgnore]
        public virtual ICollection<Teacher>? Teachers { get; set; }
        [JsonIgnore]
        public virtual Schoolinformation? Schoolinformation { get; set; }
    }
}
