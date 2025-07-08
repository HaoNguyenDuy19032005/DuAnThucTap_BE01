namespace DuAnThucTapNhom3.Models
{
    public class CreateSchoolYearDto
    {
        public string Schoolyearname { get; set; } = null!;
        public int Startyear { get; set; }
        public int Endyear { get; set; }
        public bool IsInherit { get; set; } = false;
        public int? FromSchoolYearId { get; set; }
    }
}
