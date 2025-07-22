using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Gradetype
    {
        [Key]
        public int Gradetypeid { get; set; }
        [Required(ErrorMessage = "Tên loại điểm không được để trống")]
        [StringLength(100, ErrorMessage = "Tên loại điểm tối đa 100 ký tự")]
        public string Gradetypename { get; set; } = null!;

        [Range(0.01, 100.0, ErrorMessage = "Hệ số phải nằm trong khoảng 0.01 đến 100")]
        public decimal Weightingfactor { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tối thiểu học kỳ 1 không hợp lệ")]
        public int Mininstancessemester1 { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tối thiểu học kỳ 2 không hợp lệ")]
        public int Mininstancessemester2 { get; set; }
        [JsonIgnore]
        public virtual ICollection<Grade>? Grades { get; set; }
    }

}
