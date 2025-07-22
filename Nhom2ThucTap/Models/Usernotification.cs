using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
{
    [Table("usernotifications")]
    public partial class Usernotification
    {
        [Key]
        [Column("uniqusernotificationdueid")]
        public int Uniqusernotificationdueid { get; set; }
        [Column("userid")]
        public int Userid { get; set; }
        [Column("announcementid")]
        public int Announcementid { get; set; }
        [Column("isread")]
        public bool? Isread { get; set; }
        [Column("readat", TypeName = "timestamp without time zone")]
        public DateTime? Readat { get; set; }

        [JsonIgnore]
        [ForeignKey("Announcementid")]
        [InverseProperty("Usernotifications")]
        public virtual Announcement Announcement { get; set; } = null!;
        [JsonIgnore]
        [ForeignKey("Userid")]
        [InverseProperty("Usernotifications")]
        public virtual User User { get; set; } = null!;
    }
}
