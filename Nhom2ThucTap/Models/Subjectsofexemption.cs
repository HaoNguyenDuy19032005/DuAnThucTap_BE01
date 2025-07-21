using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
{
    [Table("subjectsofexemption")]
    public partial class Subjectsofexemption
    {
        public Subjectsofexemption()
        {
            Studentexemptions = new HashSet<Studentexemption>();
        }

        [Key]
        [Column("objectid")]
        public int Objectid { get; set; }

        [Required(ErrorMessage = "Tên đối tượng miễn giảm không được để trống")]
        [Column("exemptionname")]
        public string Exemptionname { get; set; } = null!;

        [Column("description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Trạng thái hoạt động không được để trống")]
        [Column("isactive")]
        public bool? Isactive { get; set; }

        [JsonIgnore]
        [InverseProperty("Object")]
        public virtual ICollection<Studentexemption> Studentexemptions { get; set; }
    }
}
