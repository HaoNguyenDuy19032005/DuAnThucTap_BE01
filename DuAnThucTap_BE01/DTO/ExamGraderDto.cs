using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.DTO
{
    public class ExamGraderDto
    {
        [Required(ErrorMessage = "Mã lịch thi không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "Mã lịch thi không hợp lệ.")]
        public int Examscheduleid { get; set; }

        [Required(ErrorMessage = "Mã giáo viên không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "Mã giáo viên không hợp lệ.")]
        public int Teacherid { get; set; }
    }
}
