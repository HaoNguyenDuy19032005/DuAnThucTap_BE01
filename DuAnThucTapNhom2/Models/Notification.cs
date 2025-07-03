using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("notifications")]
    [Index("Notificationid", Name = "notifications_notificationid_key", IsUnique = true)]
    public partial class Notification
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("notificationid")]
        [StringLength(255)]
        public string Notificationid { get; set; } = null!;
        [Column("fk_userid")]
        [StringLength(255)]
        public string FkUserid { get; set; } = null!;
        [Column("fk_classid")]
        [StringLength(255)]
        public string FkClassid { get; set; } = null!;
        [Column("subject")]
        [StringLength(255)]
        public string Subject { get; set; } = null!;
        [Column("status")]
        [StringLength(255)]
        public string Status { get; set; } = null!;

        public virtual Class FkClass { get; set; } = null!;
        public virtual User FkUser { get; set; } = null!;
    }
}
