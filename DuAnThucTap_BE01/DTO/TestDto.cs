using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.Dtos
{
    public class TestDto
    {
        [DisplayName("ID Bài kiểm tra")]
        public int Testid { get; set; }

        [DisplayName("Tiêu đề")]
        public string Title { get; set; } = null!;

        [DisplayName("Định dạng bài kiểm tra")]
        public string? Testformat { get; set; }

        [DisplayName("Thời lượng (phút)")]
        public int? Durationinminutes { get; set; }

        [DisplayName("Thời gian bắt đầu")]
        [DataType(DataType.DateTime)]
        public DateTime? Starttime { get; set; }

        [DisplayName("Thời gian kết thúc")]
        [DataType(DataType.DateTime)]
        public DateTime? Endtime { get; set; }

        [DisplayName("Mô tả")]
        public string? Description { get; set; }

        [DisplayName("Phân loại")]
        public string? Classification { get; set; }

        [DisplayName("Đường dẫn tệp đính kèm")]
        public string? Attachmenturl { get; set; }

        [DisplayName("Yêu cầu sinh viên gửi tệp")]
        public bool Requirestudentattachment { get; set; }

        [DisplayName("Tên giáo viên")]
        public string? TeacherName { get; set; }
    }
}