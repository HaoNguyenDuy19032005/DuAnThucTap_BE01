using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuAnThucTap_BE01.Models
{
    public class ExamSchedule
    {
        [Key]
        public int ExamScheduleID { get; set; }

        [ForeignKey("Exam")]
        public int ExamID { get; set; }

        [ForeignKey("Class")]
        public int ClassID { get; set; }

        // Navigation Properties
        public virtual Exam Exam { get; set; }
        public virtual Class Class { get; set; }
        public virtual ICollection<ExamGrader> ExamGraders { get; set; }
    }
}