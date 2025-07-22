using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Subjecttype
    {
        [Key]
        public int Subjecttypeid { get; set; }
        public string Subjecttypename { get; set; } = null!;
        public bool? Isactive { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        [JsonIgnore]

        public virtual ICollection<Subject>? Subjects { get; set; }
    }
}
