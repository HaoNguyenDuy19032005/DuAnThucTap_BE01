using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Blockleader
    {
        [Key]
        public int Blockleaderid { get; set; }

        public int Gradelevelid { get; set; }
        public int Schoolyearid { get; set; }
        public int Teacherid { get; set; }

        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }

        // 🔗 Navigation properties
        [JsonIgnore]
        public virtual Gradelevel? Gradelevel { get; set; }

        [JsonIgnore]
        public virtual Schoolyear? Schoolyear { get; set; }

        [JsonIgnore]
        public virtual Teacher? Teacher { get; set; }
    }
}
