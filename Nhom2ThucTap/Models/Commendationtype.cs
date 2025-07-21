using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
{
    [Table("commendationtypes")]
    public partial class Commendationtype
    {
        public Commendationtype()
        {
            Studentcommendations = new HashSet<Studentcommendation>();
        }

        [Key]
        [Column("commendationtypeid")]
        public int Commendationtypeid { get; set; }

        [Required(ErrorMessage = "Tên loại khen thưởng không được để trống.")]
        [StringLength(255, ErrorMessage = "Tên loại khen thưởng không được vượt quá 255 ký tự.")]
        [Column("typename")]
        public string Typename { get; set; } = null!;

        [JsonIgnore]
        [InverseProperty("Commendationtype")]
        public virtual ICollection<Studentcommendation> Studentcommendations { get; set; }
    }
}
