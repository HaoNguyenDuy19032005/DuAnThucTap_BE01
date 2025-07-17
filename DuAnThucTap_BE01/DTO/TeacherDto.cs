
namespace DuAnThucTap_BE01.Dtos
{
    public class TeacherDto
    {
        public int TeacherId { get; set; }
        public string? TeacherCode { get; set; }
        public string Fullname { get; set; } = null!;
        public DateOnly? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Ethnicity { get; set; }
        public DateOnly? HireDate { get; set; }
        public string? Nationality { get; set; }
        public string? Religion { get; set; }
        public string? Status { get; set; }
        public string? Alias { get; set; }
        public string? AddressProvinceCity { get; set; }
        public string? AddressWard { get; set; }
        public string? AddressDistrict { get; set; }
        public string? AddressStreet { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public DateOnly? DateOfJoiningTheParty { get; set; }
        public string? AvatarUrl { get; set; }
        public DateOnly? DateOfJoiningGroup { get; set; }
        public bool? IsPartyMember { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? DepartmentName { get; set; }
        public string? SubjectName { get; set; }
        public string? SchoolyearName { get; set; }
    }
}