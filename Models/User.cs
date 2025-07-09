using System.Text.Json.Serialization;

namespace DuAnThucTapNhom3.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? PasswordHash { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public int LoginCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public virtual ICollection<SchoolYearlyStatus>? SchoolYearslyStatus { get; set; }
        // Navigation properties
        //public int RoleId { get; set; }
        //public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
