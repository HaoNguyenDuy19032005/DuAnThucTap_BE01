using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Nhom2ThucTap.Models
{
    [Table("announcements")]
    public partial class Announcement
    {
        public Announcement()
        {
            Usernotifications = new HashSet<Usernotification>();
        }

        [Key]
        [Column("announcementid")]
        public int Announcementid { get; set; }
        [Column("creatorid")]
        public int Creatorid { get; set; }
        [Column("title")]
        public string? Title { get; set; }
        [Column("body")]
        public string? Body { get; set; }
        [Column("targetaudience")]
        public string? Targetaudience { get; set; }
        [Column("url")]
        public string? Url { get; set; }
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime? Createdat { get; set; }

        [JsonIgnore]
        [ForeignKey("Creatorid")]
        [InverseProperty("Announcements")]
        public virtual User Creator { get; set; } = null!;
        [JsonIgnore]
        [InverseProperty("Announcement")]
        public virtual ICollection<Usernotification> Usernotifications { get; set; }
    }
}
