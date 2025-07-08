using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Topiclist
    {
        [Key]
        public int Topicid { get; set; }

        public string Topicname { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? Teachingenddate { get; set; }

        [JsonIgnore]
        public virtual ICollection<Teachingassignment> Teachingassignments { get; set; } = new List<Teachingassignment>();
    }

}
