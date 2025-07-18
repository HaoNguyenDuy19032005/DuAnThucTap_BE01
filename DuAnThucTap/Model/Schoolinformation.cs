using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Schoolinformation
    {
        public int Schoolinfoid { get; set; }
        public string Schoolname { get; set; } = null!;
        public string? Standardcode { get; set; }
        public string? Address { get; set; }
        public string? Province { get; set; }
        public string? Ward { get; set; }
        public string? District { get; set; }
        public string? Schooltype { get; set; }
        public string? Phonenumber { get; set; }
        public string? Faxnumber { get; set; }
        public string? Email { get; set; }
        public DateTime? Establishmentdate { get; set; }
        public string? Trainingmodel { get; set; }
        public string? Websiteurl { get; set; }
        public string? Principalname { get; set; }
        public string? Principalphone { get; set; }
        public string? Logourl { get; set; }
        [JsonIgnore]
        public virtual ICollection<Grade>? Grades { get; set; }
        [JsonIgnore]
        public virtual ICollection<Schoolyear>? Schoolyears { get; set; }
        [JsonIgnore]
        public virtual ICollection<Campus>? Campuses { get; set; }
    }
}
