namespace DuAnThucTap_BE01.Models
{
    public class TopicListDto
    {
        public Guid Topicid { get; set; }
        public string Topicname { get; set; } = null!;
        public string? Description { get; set; }
        public string? Teachingenddate { get; set; }
    }
}