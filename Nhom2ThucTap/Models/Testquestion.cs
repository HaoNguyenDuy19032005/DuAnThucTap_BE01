//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore;

//namespace Nhom2ThucTap.Models
//{
//    [Table("testquestions")]
//    public partial class Testquestion
//    {
//        //public Testquestion()
//        //{
//        //    Studenttestanswers = new HashSet<Studenttestanswer>();
//        //}

//        [Key]
//        [Column("questionid")]
//        public int Questionid { get; set; }
//        [Column("testid")]
//        public int Testid { get; set; }
//        [Column("title")]
//        public string Title { get; set; } = null!;
//        [Column("description")]
//        public string? Description { get; set; }
//        [Column("attachmenturl")]
//        public string? Attachmenturl { get; set; }
//        [Column("displayorder")]
//        public int? Displayorder { get; set; }
//        [Column("optiona")]
//        public string? Optiona { get; set; }
//        [Column("optionb")]
//        public string? Optionb { get; set; }
//        [Column("optionc")]
//        public string? Optionc { get; set; }
//        [Column("optiond")]
//        public string? Optiond { get; set; }
//        [Column("correctoption")]
//        public string? Correctoption { get; set; }
//        [Column("points")]
//        public int? Points { get; set; }

//        [ForeignKey("Testid")]
//        [InverseProperty("Testquestions")]
//        public virtual Test Test { get; set; } = null!;
//        //[InverseProperty("Question")]
//        //public virtual ICollection<Studenttestanswer> Studenttestanswers { get; set; }
//    }
//}
