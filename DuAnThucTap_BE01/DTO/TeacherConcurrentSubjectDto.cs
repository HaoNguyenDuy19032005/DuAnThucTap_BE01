namespace DuAnThucTap_BE01.DTO
{
    public class TeacherConcurrentSubjectDto
    {
        public int TeacherId { get; set; }
        public string? TeacherName { get; set; }

        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }

        public int SchoolyearId { get; set; }
        public string? SchoolyearName { get; set; }
    }
}
