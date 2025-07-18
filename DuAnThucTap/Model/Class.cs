using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Class
    {
        [Key]
        public int Classid { get; set; }

        [Required(ErrorMessage = "Tên lớp không được để trống.")]
        [StringLength(100, MinimumLength = 1)]
        public string Classname { get; set; } = null!;

        [Required(ErrorMessage = "Số lượng lớp không được để trống.")]
        public int? Maxstudents { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống.")]
        public string? Description { get; set; }

        public int? Schoolyearid { get; set; }
        public int? Gradelevelid { get; set; }
        public int? Classtypeid { get; set; }
        public int? Teacherid { get; set; }

        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }

        // Quan hệ nhiều môn học
        public virtual ICollection<ClassSubject>? ClassSubjects { get; set; }

        [JsonIgnore]
        public virtual Classtype? Classtype { get; set; }
        [JsonIgnore]
        public virtual Gradelevel? Gradelevel { get; set; }
        [JsonIgnore]
        public virtual Schoolyear? Schoolyear { get; set; }
        [JsonIgnore]
        public virtual Teacher? Teacher { get; set; }
    }

    public class ClassSubject
    {
        [Key]
        public int Classid { get; set; }
        public int Subjectid { get; set; }

        [JsonIgnore]
        public virtual Class? Class { get; set; }
        [JsonIgnore]
        public virtual Subject? Subject { get; set; }
    }
    public class CreateClassDto
    {
        [Required(ErrorMessage = "Tên lớp không được để trống.")]
        [StringLength(100)]
        public string Classname { get; set; } = null!;

        [Required(ErrorMessage = "Số lượng tối đa không được để trống.")]
        [Range(30, 45, ErrorMessage = "Số lượng học sinh phải nằm trong khoảng từ 30 đến 45.")]
        public int? Maxstudents { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống.")]
        public string? Description { get; set; }

        public int? Schoolyearid { get; set; }
        public int? Gradelevelid { get; set; }
        public int? Classtypeid { get; set; }
        public int? Teacherid { get; set; }

        public List<int>? SubjectIds { get; set; } // nhiều môn học
    }


}
