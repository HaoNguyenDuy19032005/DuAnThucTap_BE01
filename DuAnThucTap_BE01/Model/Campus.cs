using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Campus
    {
        [Key]
        public int Campusid { get; set; }
        public int Schoolinfoid { get; set; }

        public string? Campusname { get; set; }
        public string? Address { get; set; }
        public string? Phonenumber { get; set; }
        public string? Imageurl { get; set; }

        public string? Contactpersonname { get; set; }
        public string? Contactpersonmobile { get; set; }
        public string? Contactpersonemail { get; set; }

        [JsonIgnore]
        public virtual Schoolinformation? Schoolinformation { get; set; }
    }
}
