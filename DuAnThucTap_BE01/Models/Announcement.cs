using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
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
        public Guid Announcementid { get; set; }
        [Column("creatorid")]
        public Guid Creatorid { get; set; }
        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; } = null!;
        [Column("body")]
        public string? Body { get; set; }
        [Column("targetaudience")]
        public string? Targetaudience { get; set; }
        [Column("url")]
        public string? Url { get; set; }
        [Column("createdat")]
        public DateTime? Createdat { get; set; }

        [ForeignKey("Creatorid")]
        [InverseProperty("Announcements")]
        public virtual User Creator { get; set; } = null!;
        [InverseProperty("Announcement")]
        public virtual ICollection<Usernotification> Usernotifications { get; set; }
    }
}
