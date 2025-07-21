using System;

using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;



namespace Nhom2ThucTap.Models

{

    [Table("displayedtestlist")]

    public class DisplayedTestList

    {

        [Key]

        [Column("displayitemid")]

        public int DisplayItemID { get; set; }



        [Required]

        [Column("subjectid")]

        public int SubjectID { get; set; }



        [Required]

        [Column("teacherid")]

        public int TeacherID { get; set; }



        [Required]

        [Column("title")]

        public string Title { get; set; } = null!;



        [Required]

        [Column("starttime")]

        public DateTime StartTime { get; set; }



        [Required]

        [Column("durationinminutes")]

        public int DurationInMinutes { get; set; }



        [Column("statusdisplay")]

        public string? StatusDisplay { get; set; }



        [Column("actiondisplay")]

        public string? ActionDisplay { get; set; }



        [Column("createdat")]

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



        // Điều hướng đến các bảng liên quan (Subjects, Teachers)

        [JsonIgnore]

        [ForeignKey("SubjectID")]

        public virtual Subject? Subject { get; set; }



        [JsonIgnore]

        [ForeignKey("TeacherID")]

        public virtual Teacher? Teacher { get; set; }

    }

}

