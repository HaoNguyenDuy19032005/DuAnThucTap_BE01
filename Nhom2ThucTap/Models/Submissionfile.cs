//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

//namespace Nhom2ThucTap.Models
//{
//    [Table("submissionfiles")]
//    public partial class Submissionfile
//    {
//        [Key]
//        [Column("fileid")]
//        public int Fileid { get; set; }
//        [Column("submissionid")]
//        public int Submissionid { get; set; }
//        [Column("filename")]
//        public string? Filename { get; set; }
//        [Column("fileurl")]
//        public string? Fileurl { get; set; }
//        [Column("filesizekb")]
//        public int? Filesizekb { get; set; }

//        [ForeignKey("Submissionid")]
//        [InverseProperty("Submissionfiles")]
//        public virtual Studenttestsubmission Submission { get; set; } = null!;
//    }
//}
