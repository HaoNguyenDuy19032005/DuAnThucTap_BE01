namespace DuAnThucTap_BE01.DTO

{
    public class TeacherWorkStatusHistoryDto
    {
        public int Historyid { get; set; }
        public int Teacherid { get; set; }
        public string? TeacherName { get; set; }
        public string Statustype { get; set; } = null!;
        public DateOnly? Startdate { get; set; }
        public DateOnly? Enddate { get; set; }
        public string? Note { get; set; }
        public string? Decisionfileurl { get; set; }
        public DateTime? Createdat { get; set; }
    }
}