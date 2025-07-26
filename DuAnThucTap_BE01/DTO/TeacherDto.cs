using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.Dtos
{
    public class TeacherDto
    {
        [DisplayName("ID Giáo viên")]
        public int TeacherId { get; set; }

        [DisplayName("Mã giáo viên")]
        public string? TeacherCode { get; set; }

        [DisplayName("Họ và tên")]
        public string Fullname { get; set; } = null!;

        [DisplayName("Ngày sinh")]
        [DataType(DataType.Date)]
        public DateOnly? DateOfBirth { get; set; }

        [DisplayName("Giới tính")]
        public string? Gender { get; set; }

        [DisplayName("Dân tộc")]
        public string? Ethnicity { get; set; }

        [DisplayName("Ngày vào làm")]
        [DataType(DataType.Date)]
        public DateOnly? HireDate { get; set; }

        [DisplayName("Quốc tịch")]
        public string? Nationality { get; set; }

        [DisplayName("Tôn giáo")]
        public string? Religion { get; set; }

        [DisplayName("Tình trạng công tác")]
        public string? Status { get; set; }

        [DisplayName("Bí danh")]
        public string? Alias { get; set; }

        [DisplayName("Địa chỉ: Tỉnh/Thành phố")]
        public string? AddressProvinceCity { get; set; }

        [DisplayName("Địa chỉ: Phường/Xã")]
        public string? AddressWard { get; set; }

        [DisplayName("Địa chỉ: Quận/Huyện")]
        public string? AddressDistrict { get; set; }

        [DisplayName("Địa chỉ: Số nhà, tên đường")]
        public string? AddressStreet { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; } = null!;

        [DisplayName("Số điện thoại")]
        public string? PhoneNumber { get; set; }

        [DisplayName("Ngày vào Đảng")]
        [DataType(DataType.Date)]
        public DateOnly? DateOfJoiningTheParty { get; set; }

        [DisplayName("Đường dẫn ảnh đại diện")]
        public string? AvatarUrl { get; set; }

        [DisplayName("Ngày vào Đoàn")]
        [DataType(DataType.Date)]
        public DateOnly? DateOfJoiningGroup { get; set; }

        [DisplayName("Là Đảng viên")]
        public bool? IsPartyMember { get; set; }

        [DisplayName("Ngày tạo")]
        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; }



        [DisplayName("Ngày cập nhật")]
        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }

        [DisplayName("Tên khoa/bộ môn")]
        public string? DepartmentName { get; set; }

        [DisplayName("Tên môn học chính")]
        public string? SubjectName { get; set; }

        [DisplayName("Tên năm học")]
        public string? SchoolyearName { get; set; }
    }
}