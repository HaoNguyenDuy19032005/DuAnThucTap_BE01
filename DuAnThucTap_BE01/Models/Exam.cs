using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DuAnThucTap_BE01.Models
{
    public partial class Exam
    {
        public int ExamID { get; set; }
        public int SchoolYearID { get; set; }
        public int GradeLevelID { get; set; }
        public int SemesterID { get; set; }
        public int SubjectID { get; set; }
        public string ExamName { get; set; } = null!;
        public DateTime ExamDate { get; set; }
        public int DurationMinutes { get; set; }
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public virtual Schoolyear? Schoolyear { get; set; } = null!;
        [JsonIgnore]
        public virtual Gradelevel? Gradelevel { get; set; } = null!;
        [JsonIgnore]
        public virtual Semester? Semester { get; set; } = null!;
        [JsonIgnore]
        public virtual Subject? Subject { get; set; } = null!;

    }
}
