using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.Dtos
{
    public class TestAssignmentDto
    {
        [DisplayName("ID Phân công")]
        public int Assignmentid { get; set; }

        [DisplayName("Tiêu đề bài kiểm tra")]
        public string TestTitle { get; set; } = null!;

        [DisplayName("Tên lớp")]
        public string ClassName { get; set; } = null!;

        [DisplayName("Trạng thái")]
        [StringLength(50)]
        public string? Status { get; set; }
    }
}