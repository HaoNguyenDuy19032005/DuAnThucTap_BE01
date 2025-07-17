namespace DuAnThucTap_BE01.DTO
{
    public class TeacherTrainingHistoryDto
    {
        public int Trainingid { get; set; }
        public int Teacherid { get; set; }
        public string? TeacherName { get; set; } 
        public string? Traininginstitutionname { get; set; }
        public string? Majororspecialization { get; set; }
        public DateOnly? Startdate { get; set; }
        public string? Enddateorgraduationyear { get; set; }
        public bool? Active { get; set; }
        public string? Trainingtype { get; set; }
        public string? Certificatediplomaname { get; set; }
        public string? Attachmenturl { get; set; }
    }
}
