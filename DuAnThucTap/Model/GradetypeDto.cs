using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap.DTOs
{
    public class GradetypeDto
    {
        [Required(ErrorMessage = "Tên loại điểm không được để trống")]
        public string Gradetypename { get; set; } = null!;

        [Required(ErrorMessage = "Hệ số điểm là bắt buộc")]
        [Range(1, 10, ErrorMessage = "Hệ số điểm phải nằm trong khoảng 1 đến 10")]
        public decimal? Weightingfactor { get; set; }

        [Required(ErrorMessage = "Số lượng tối thiểu học kỳ 1 là bắt buộc")]
        [Range(1, 10, ErrorMessage = "Tối thiểu học kỳ 1 phải nằm trong 1-10")]
        public int? Mininstancessemester1 { get; set; }

        [Required(ErrorMessage = "Số lượng tối thiểu học kỳ 2 là bắt buộc")]
        [Range(1, 10, ErrorMessage = "Tối thiểu học kỳ 2 phải nằm trong 1-10")]
        public int? Mininstancessemester2 { get; set; }
    }
}
