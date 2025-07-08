using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuAnThucTap_BE01.Models
{
    public class ExamGrader
    {
        [Key]
        public int ExamGraderID { get; set; }

        [ForeignKey("ExamSchedule")]
        public int ExamScheduleID { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherID { get; set; }

        // Navigation Properties
        public virtual ExamSchedule ExamSchedule { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}