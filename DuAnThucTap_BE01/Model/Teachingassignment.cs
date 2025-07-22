using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Teachingassignment
    {
        [Key]
        public int Assignmentid { get; set; }
        public int Teacherid { get; set; }
        public int Subjectid { get; set; }
        public int? Classtypeid { get; set; }
        public int? Topicid { get; set; }
        public int Schoolyearid { get; set; }
        public DateTime? Teachingstartdate { get; set; }
        public DateTime? Teachingenddate { get; set; }
        public string? Notes { get; set; }

        [JsonIgnore]
        public virtual Topiclist? Topic { get; set; } = null!;
        [JsonIgnore]
        public virtual Classtype? Classtype { get; set; }
        [JsonIgnore]
        public virtual Schoolyear? Schoolyear { get; set; } = null!;
        [JsonIgnore]
        public virtual Subject? Subject { get; set; } = null!;
        [JsonIgnore]
        public virtual Teacher? Teacher { get; set; } = null!;
    }
}
