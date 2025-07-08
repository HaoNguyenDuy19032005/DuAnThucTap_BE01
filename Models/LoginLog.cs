namespace DuAnThucTapNhom3.Models
{
    public class LoginLog
    {
        public int LoginLogId { get; set; }
        public int UserId { get; set; }
        public DateTime LoginTime { get; set; }

        // Navigation
        public virtual User User { get; set; } = null!;
    }
}
