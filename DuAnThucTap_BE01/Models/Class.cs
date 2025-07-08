using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuAnThucTap_BE01.Models
{
	public class Class
	{
		[Key]
		public int ClassID { get; set; }

		public string ClassName { get; set; }
		public int MaxStudents { get; set; }
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public int SchoolYearID { get; set; }
		public int GradeLevelID { get; set; }
		public int ClassTypeID { get; set; }
		public int TeacherID { get; set; }
		public int SubjectID { get; set; }

		// Navigation Properties
		public virtual Teacher Teacher { get; set; }
		public virtual ICollection<ExamSchedule> ExamSchedules { get; set; }
	}
}