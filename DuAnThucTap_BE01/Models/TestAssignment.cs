namespace DuAnThucTap_BE01.Models
{
    public class TestAssignment
    {
        public int AssignmentId { get; set; }
        public int TestId { get; set; }
        public int ClassId { get; set; }
        public string Status { get; set; } = null!;
    }
}