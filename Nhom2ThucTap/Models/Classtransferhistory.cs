using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
{
    [Table("classtransferhistory")]
    public partial class Classtransferhistory
    {
        [Key]
        [Column("transferid")]
        public int Transferid { get; set; }

        [Required(ErrorMessage = "Mã học sinh không được để trống.")]
        [Column("studentid")]
        public int Studentid { get; set; }

        [Required(ErrorMessage = "Lớp chuyển đi không được để trống.")]
        [Column("fromclassid")]
        public int Fromclassid { get; set; }

        [Required(ErrorMessage = "Lớp chuyển đến không được để trống.")]
        [Column("toclassid")]
        public int Toclassid { get; set; }

        [Required(ErrorMessage = "Học kỳ không được để trống.")]
        [Column("semesterid")]
        public int Semesterid { get; set; }

        [StringLength(500, ErrorMessage = "Lý do không được vượt quá 500 ký tự.")]
        [Column("reason")]
        public string? Reason { get; set; }

        [StringLength(300, ErrorMessage = "Tệp đính kèm không được vượt quá 300 ký tự.")]
        [Column("attachmenturl")]
        public string? Attachmenturl { get; set; }

        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime? Createdat { get; set; }

        // Navigation properties
        [ForeignKey("Fromclassid")]
        [InverseProperty("ClasstransferhistoryFromclasses")]
        [JsonIgnore]
        public virtual Class? Fromclass { get; set; }

        [ForeignKey("Semesterid")]
        [InverseProperty("Classtransferhistories")]
        [JsonIgnore]
        public virtual Semester? Semester { get; set; }

        [ForeignKey("Studentid")]
        [InverseProperty("Classtransferhistories")]
        [JsonIgnore]
        public virtual Student? Student { get; set; }

        [ForeignKey("Toclassid")]
        [InverseProperty("ClasstransferhistoryToclasses")]
        [JsonIgnore]
        public virtual Class? Toclass { get; set; }
    }
}
