using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTap_BE01.Models
{
    [Table("usernotifications")]
    [Index("Userid", "Announcementid", Name = "usernotifications_userid_announcementid_key", IsUnique = true)]
    public partial class Usernotification
    {
        [Key]
        [Column("usernotificationid")]
        public int Usernotificationid { get; set; }
        [Column("userid")]
        public int Userid { get; set; }
        [Column("announcementid")]
        public int Announcementid { get; set; }
        [Column("isread")]
        public bool Isread { get; set; }
        [Column("readat")]
        public DateTime? Readat { get; set; }

        [ForeignKey("Announcementid")]
        [InverseProperty("Usernotifications")]
        public virtual Announcement Announcement { get; set; } = null!;
        [ForeignKey("Userid")]
        [InverseProperty("Usernotifications")]
        public virtual User User { get; set; } = null!;
    }
}
