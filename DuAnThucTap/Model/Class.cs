using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Class
    {
        [Key]
        public int Classid { get; set; }
        public string Classname { get; set; } = null!;
        public int? Maxstudents { get; set; }
        public string? Description { get; set; }
        public int? Schoolyearid { get; set; }
        public int? Gradelevelid { get; set; }
        public int? Classtypeid { get; set; }
        public int? Teacherid { get; set; }
        public int? Subjectid { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        [JsonIgnore]
        public virtual Classtype? Classtype { get; set; }
        [JsonIgnore]
        public virtual Gradelevel? Gradelevel { get; set; }
        [JsonIgnore]
        public virtual Schoolyear? Schoolyear { get; set; }
        [JsonIgnore]
        public virtual Subject? Subject { get; set; }
        [JsonIgnore]
        public virtual Teacher? Teacher { get; set; }
      
    }
}
