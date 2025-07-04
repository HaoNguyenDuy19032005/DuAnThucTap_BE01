using System.ComponentModel.DataAnnotations;

namespace DuAnThucTap_BE01.Models
{
    public class TopicList
    {
        [Key]
        public int TopicID { get; set; }
        public string TopicName { get; set; } = null!;
        public string? Decription { get; set; }
        public DateTime TeachingEndDate { get; set; }

        public virtual ICollection<TeachingAssignment> TeachingAssignments { get; set; }
    }

}
