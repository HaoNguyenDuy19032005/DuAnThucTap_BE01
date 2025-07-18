using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("topiclist")]
    public partial class Topiclist
    {
        public Topiclist()
        {
            Teachingassignments = new HashSet<Teachingassignment>();
        }

        [Key]
        [Column("topicid")]
        public int Topicid { get; set; }

        [Column("topicname")]
        [StringLength(255)]
        // Thêm [Required] để đảm bảo dữ liệu đầu vào luôn có tên chủ đề
        [Required(ErrorMessage = "Tên chủ đề không được để trống")]
        public string Topicname { get; set; } = null!;

        [Column("description")]
        public string? Description { get; set; }

        [Column("teachingenddate")]
        public DateOnly? Teachingenddate { get; set; }

        [JsonIgnore]
        [InverseProperty("Topic")]
        public virtual ICollection<Teachingassignment> Teachingassignments { get; set; }
    }
}