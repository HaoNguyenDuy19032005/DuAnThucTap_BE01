using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTapNhom3.Models
{
    public class StudentSemesterSummary
    {
        [Key]
        public int SummaryId { get; set; }
        public double AverageScore { get; set; }
        [JsonIgnore]
        public ICollection<Student> StudentsInSemeterSumamryModel { get; set; } = new List<Student>();
        
        public int SemesterId { get; set; }
        [JsonIgnore]
        public Semester Semester { get; set; } = null!;
    }
}
