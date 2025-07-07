namespace DuAnThucTap_BE01.Models
{
    public class TeachingAssignmentDto
    {
        public Guid Assignmentid { get; set; }
        public Guid Teacherid { get; set; }
        public Guid Subjectid { get; set; }
        public Guid? Classtypeid { get; set; }
        public Guid? Topicid { get; set; }
        public Guid Schoolyearid { get; set; }
        public string? Teachingstartdate { get; set; }
        public string? Teachingenddate { get; set; }
        public string? Notes { get; set; }
    }
}