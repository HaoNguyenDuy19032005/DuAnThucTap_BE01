using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Departmentleader
    {
        [Key]
        public int Departmentleaderid { get; set; }
        public int Departmentid { get; set; }
        public int Schoolyearid { get; set; }
        public int Teacherid { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        [JsonIgnore]
        public virtual Department? Department { get; set; } = null!;
        [JsonIgnore]
        public virtual Schoolyear? Schoolyear { get; set; } = null!;
        [JsonIgnore]
        public virtual Teacher? Teacher { get; set; } = null!;
    }

    public class DepartmentleaderCreateDto
    {
        [Required(ErrorMessage = "Departmentid không được bỏ trống")]
        public int Departmentid { get; set; }

        [Required(ErrorMessage = "Schoolyearid không được bỏ trống")]
        public int Schoolyearid { get; set; }

        [Required(ErrorMessage = "Teacherid không được bỏ trống")]
        public int Teacherid { get; set; }

        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
    }

    public class PaginationDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

}
