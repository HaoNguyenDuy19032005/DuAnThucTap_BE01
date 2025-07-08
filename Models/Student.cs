namespace DuAnThucTapNhom3.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public string StudentCode { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Gender { get; set; } = null!;
        public Class? Class { get; set; }
        public StudentSemesterSummary? StudentSemesterSummarys { get; set; }
    }
}
