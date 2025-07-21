using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
{
    [Table("disciplinetypes")]
    public partial class Disciplinetype
    {
        public Disciplinetype()
        {
            Studentdisciplines = new HashSet<Studentdiscipline>();
        }

        [Key]
        [Column("disciplinetypeid")]
        public int Disciplinetypeid { get; set; }

        [Required(ErrorMessage = "Tên loại kỷ luật không được để trống.")]
        [StringLength(255, ErrorMessage = "Tên loại kỷ luật không được vượt quá 255 ký tự.")]
        [Column("typename")]
        public string? Typename { get; set; }

        [Required(ErrorMessage = "Mức độ nghiêm trọng không được để trống.")]
        [StringLength(100, ErrorMessage = "Mức độ nghiêm trọng không được vượt quá 100 ký tự.")]
        [Column("severity")]
        public string? Severity { get; set; }

        [JsonIgnore]
        [InverseProperty("Disciplinetype")]
        public virtual ICollection<Studentdiscipline> Studentdisciplines { get; set; }
    }
}
