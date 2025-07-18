using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Subject
    {
        [Key]
        public int Subjectid { get; set; }

        [Required(ErrorMessage = "Tên môn học không được để trống")]
        public string Subjectname { get; set; } = null!;
        [Required(ErrorMessage = "Mã môn học không được để trống")]
        public string Subjectcode { get; set; }
        [Required(ErrorMessage = "Số tiết môn học không được để trống")]
        public int? Defaultperiodssem1 { get; set; }
        [Required(ErrorMessage = "Số tiết môn học không được để trống")]
        public int? Defaultperiodssem2 { get; set; }

        [Required(ErrorMessage = "Mã khoa không được để trống")]
        public int? Departmentid { get; set; }

        [Required(ErrorMessage = "Mã loại môn học không được để trống")]
        public int? Subjecttypeid { get; set; }

        [Required(ErrorMessage = "Mã năm học không được để trống")]
        public int? Schoolyearid { get; set; }

        public DateTime? Createdat { get; set; }
        public DateTime? Updatedat { get; set; }

        // Navigation Properties
        [JsonIgnore] public virtual Department? Department { get; set; }
        [JsonIgnore] public virtual Schoolyear? Schoolyear { get; set; }
        [JsonIgnore] public virtual Subjecttype? Subjecttype { get; set; }
        [JsonIgnore] public virtual ICollection<Class>? Classes { get; set; }
        [JsonIgnore] public virtual ICollection<Grade>? Grades { get; set; }
        [JsonIgnore] public virtual ICollection<Teacher>? Teachers { get; set; }
        [JsonIgnore] public virtual ICollection<Teachingassignment>? Teachingassignments { get; set; }
        [JsonIgnore] public virtual ICollection<ClassSubject>? ClassSubjects { get; set; }
    }

}
