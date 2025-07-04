using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.Models
{
    public class TeachingAssignment
    {
        [Key]
        public int AssignmentID { get; set; }

        public int TeacherID { get; set; }
        public int SubjectID { get; set; }
        public int ClassTypeID { get; set; }
        public DateTime TeachingStartDate { get; set; }
        public DateTime TeachingEndDate { get; set; }
        public string? Notes { get; set; }

        public int TopicID { get; set; }
        public int SchoolYearID { get; set; }
        public int DepartmentID { get; set; }

        // Navigation
        public virtual TopicList Topic { get; set; }
    }
}
