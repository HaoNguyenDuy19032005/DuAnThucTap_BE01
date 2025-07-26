namespace DuAnThucTap_BE01.DTOs
{
    public class TopicListDto
    {
        public int Topicid { get; set; }
        public string? Topicname { get; set; }
        public string? Description { get; set; }
        public DateOnly? Teachingenddate { get; set; }
    }
}