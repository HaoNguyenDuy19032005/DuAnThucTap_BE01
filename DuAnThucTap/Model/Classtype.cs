using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Classtype
    {
        [Key]
        public int Classtypeid { get; set; }
        public string Classtypename { get; set; } = null!;
        public bool? Isactive { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        [JsonIgnore]
        public virtual ICollection<Class>? Classes { get; set; }
        [JsonIgnore]
        public virtual ICollection<Teachingassignment>? Teachingassignments { get; set; }
    }
}
