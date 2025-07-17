using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.DTO
{
    public class TeacherConcurrentSubjectRequestDto
    {
        [Required(ErrorMessage = "ID Giáo viên không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Giáo viên không hợp lệ.")]
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "ID Môn học không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Môn học không hợp lệ.")]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "ID Năm học không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID Năm học không hợp lệ.")]
        public int SchoolYearId { get; set; }
    }
}
