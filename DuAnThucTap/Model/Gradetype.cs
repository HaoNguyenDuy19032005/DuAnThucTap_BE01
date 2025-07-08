using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Gradetype
    {
        [Key]
        public int Gradetypeid { get; set; }
        public string Gradetypename { get; set; } = null!;
        public decimal Weightingfactor { get; set; }
        public int Mininstancessemester1 { get; set; }
        public int Mininstancessemester2 { get; set; }

        [JsonIgnore]
        public virtual ICollection<Grade>? Grades { get; set; }
    }
}
