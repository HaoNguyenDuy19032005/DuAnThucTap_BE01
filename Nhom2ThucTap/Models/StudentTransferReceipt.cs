using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Nhom2ThucTap.Models
{
    [Table("studenttransferreceipt")]
    public partial class StudentTransferReceipt
    {
        [Key]
        [Column("receiptid")]
        public int ReceiptId { get; set; }

        [Required(ErrorMessage = "Họ tên học sinh không được để trống")]
        [Column("studentname")]
        public string StudentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mã học sinh không được để trống")]
        [Column("studentcode")]
        public string StudentCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ngày chuyển trường không được để trống")]
        [Column("transferdate", TypeName = "date")]
        public DateTime? TransferDate { get; set; }

        [Required(ErrorMessage = "Học kỳ không được để trống")]
        [Column("semesterid")]
        public int? SemesterId { get; set; }

        [Required(ErrorMessage = "Tỉnh không được để trống")]
        [Column("province")]
        public string Province { get; set; } = string.Empty;

        [Required(ErrorMessage = "Huyện không được để trống")]
        [Column("district")]
        public string District { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên trường chuyển đến không được để trống")]
        [Column("fromschool")]
        public string FromSchool { get; set; } = string.Empty;

        [Column("reason")]
        public string? Reason { get; set; }

        [Required(ErrorMessage = "File đính kèm không được để trống")]
        [Column("attachmentfile")]
        public string AttachmentFile { get; set; } = string.Empty;

        [JsonIgnore]
        [ForeignKey("SemesterId")]
        [InverseProperty("StudentTransferReceipts")]
        public virtual Semester? Semester { get; set; }
    }
}
