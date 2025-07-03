using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("themes")]
    [Index("Themeid", Name = "themes_themeid_key", IsUnique = true)]
    public partial class Theme
    {
        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("themeid")]
        [StringLength(255)]
        public string Themeid { get; set; } = null!;
        [Column("themename")]
        [StringLength(255)]
        public string Themename { get; set; } = null!;
        [Column("thumbnailid")]
        [StringLength(255)]
        public string Thumbnailid { get; set; } = null!;

        public virtual File Thumbnail { get; set; } = null!;
    }
}
