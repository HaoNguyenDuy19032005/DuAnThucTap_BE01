using System.ComponentModel.DataAnnotations;

namespace DuAnThucTapNhom3.Models
{
    public class StudentSemesterSummary
    {
        [Key]
        public int SummaryId { get; set; }
        public double AverageScore { get; set; }
        public ICollection<Student> students { get; set; } = new List<Student>();
        public int SemesterId { get; set; }
        public Semester Semester { get; set; } = null!;
    }
}
