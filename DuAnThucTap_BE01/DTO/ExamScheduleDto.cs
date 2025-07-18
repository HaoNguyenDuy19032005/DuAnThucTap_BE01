using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.DTO
{
    public class ExamScheduleDto
    {
        [Required(ErrorMessage = "Mã kỳ thi không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "Mã kỳ thi không hợp lệ.")]
        public int Examid { get; set; }

        [Required(ErrorMessage = "Mã lớp học không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "Mã lớp học không hợp lệ.")]
        public int Classid { get; set; }
    }
}
