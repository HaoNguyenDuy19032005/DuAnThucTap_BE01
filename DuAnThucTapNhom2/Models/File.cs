using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DuAnThucTapNhom2.Models
{
    [Table("files")]
    [Index("Fileid", Name = "files_fileid_key", IsUnique = true)]
    public partial class File
    {
        public File()
        {
            Themes = new HashSet<Theme>();
        }

        [Key]
        [Column("pk")]
        public int Pk { get; set; }
        [Column("fileid")]
        [StringLength(255)]
        public string Fileid { get; set; } = null!;
        [Column("originalfilename")]
        [StringLength(255)]
        public string Originalfilename { get; set; } = null!;
        [Column("storedfilepath")]
        [StringLength(255)]
        public string Storedfilepath { get; set; } = null!;
        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; } = null!;
        [Required]
        [Column("isactive")]
        public bool? Isactive { get; set; }
        [Column("createdat", TypeName = "timestamp without time zone")]
        public DateTime Createdat { get; set; }
        [Column("updatedat", TypeName = "timestamp without time zone")]
        public DateTime Updatedat { get; set; }
        [Column("fk_uploadedbyuserid")]
        [StringLength(255)]
        public string FkUploadedbyuserid { get; set; } = null!;
        [Column("fk_classid")]
        [StringLength(255)]
        public string FkClassid { get; set; } = null!;
        [Column("fk_studentid")]
        [StringLength(255)]
        public string FkStudentid { get; set; } = null!;
        [Column("filetype")]
        [StringLength(255)]
        public string Filetype { get; set; } = null!;
        [Column("filesizekb")]
        public double Filesizekb { get; set; }
        [Column("uploadtimestamp", TypeName = "timestamp without time zone")]
        public DateTime Uploadtimestamp { get; set; }

        public virtual Class FkClass { get; set; } = null!;
        public virtual Student FkStudent { get; set; } = null!;
        public virtual User FkUploadedbyuser { get; set; } = null!;
        public virtual ICollection<Theme> Themes { get; set; }
    }
}
