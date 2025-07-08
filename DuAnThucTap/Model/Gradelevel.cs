using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Gradelevel
    {
        [Key]
        public int Gradelevelid { get; set; }
        public string Gradelevelname { get; set; } = null!;
        public string? Codegradelevel { get; set; }
        public int? Teacherid { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        [JsonIgnore]
        public virtual Teacher? Teacher { get; set; }
        [JsonIgnore]
        public virtual ICollection<Class>? Classes { get; set; }
    }
}
