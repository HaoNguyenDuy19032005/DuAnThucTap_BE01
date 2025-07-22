using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DuAnThucTap.Model
{
    public class Grade
    {
        [Key]
        public int Gradeid { get; set; }
        public int Studentid { get; set; }
        public int Subjectid { get; set; }
        public int Semesterid { get; set; }
        public int Gradetypeid { get; set; }
        public int Schoolid { get; set; }
        public decimal Score { get; set; }
        public int Instance { get; set; }
        public DateTime? Gradeddate { get; set; }
        [JsonIgnore]
        public virtual Gradetype? Gradetype { get; set; }
        [JsonIgnore]
        public virtual Schoolinformation? School { get; set; }
        [JsonIgnore]
        public virtual Semester? Semester { get; set; }
   
        public virtual Subject? Subject { get; set; }
    }
}
