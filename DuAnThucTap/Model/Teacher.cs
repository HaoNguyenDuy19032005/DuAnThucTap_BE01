using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Teacher
    {
        [Key]
        public int Teacherid { get; set; }
        public string? Teachercode { get; set; }
        public string Fullname { get; set; } = null!;
        public DateTime? Dateofbirth { get; set; }
        public string? Gender { get; set; }
        public string? Ethnicity { get; set; }
        public DateTime? Hiredate { get; set; }
        public string? Nationality { get; set; }
        public string? Religion { get; set; }
        public string? Status { get; set; }
        public string? Alias { get; set; }
        public string? AddressProvincecity { get; set; }
        public string? AddressWard { get; set; }
        public string? AddressDistrict { get; set; }
        public string? AddressStreet { get; set; }
        public string Email { get; set; } = null!;
        public string? Phonenumber { get; set; }
        public DateTime? Dateofjoiningtheparty { get; set; }
        public DateTime? Dateofjoininggroup { get; set; }
        public bool? Ispartymember { get; set; }
        public int? Departmentid { get; set; }
        public int? Subjectid { get; set; }
        public int? Schoolyearid { get; set; }
    
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        [JsonIgnore]
        public virtual Department? Department { get; set; }
        [JsonIgnore]
        public virtual Schoolyear? Schoolyear { get; set; }
        [JsonIgnore]
        public virtual Subject? Subject { get; set; }
        [JsonIgnore]
        public virtual ICollection<Class>? Classes { get; set; }
        [JsonIgnore]
        public virtual ICollection<Blockleader>? Blockleaders { get; set; }
    }
}
