using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Department
    {
        [Key]
        public int Departmentid { get; set; }
        public string Departmentname { get; set; } = null!;
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }

        [JsonIgnore]
        public virtual ICollection<Subject>? Subjects { get; set; }
        [JsonIgnore]
        public virtual ICollection<Teacher>? Teachers { get; set; }
    }
}
