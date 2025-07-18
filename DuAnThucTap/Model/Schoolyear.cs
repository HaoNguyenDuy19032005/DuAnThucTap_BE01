using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Schoolyear
    {
        [Key]
        public int Schoolyearid { get; set; }
        [Required(ErrorMessage = "Schoolinfoid không được bỏ trống")]
        public int Schoolinfoid { get; set; }

        [Required(ErrorMessage = "Tên năm học không được bỏ trống")]
        public string Schoolyearname { get; set; } = null!;

        [Required(ErrorMessage = "Năm bắt đầu không được bỏ trống")]
        public int Startyear { get; set; }

        [Required(ErrorMessage = "Năm kết thúc không được bỏ trống")]
        public int Endyear { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }
        [JsonIgnore]
        public virtual ICollection<Class>? Classes { get; set; }
        [JsonIgnore]
        public virtual ICollection<Semester>? Semesters { get; set; }
        [JsonIgnore]
        public virtual ICollection<Subject>? Subjects { get; set; }
        [JsonIgnore]
        public virtual ICollection<Teacher>? Teachers { get; set; }
        [JsonIgnore]
        public virtual Schoolinformation? Schoolinformation { get; set; }
        [JsonIgnore]
        public virtual Blockleader? Blockleaders { get; set; }
    }
}
