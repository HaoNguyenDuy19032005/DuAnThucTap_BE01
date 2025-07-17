namespace DuAnThucTap_BE01.DTO
{
    public class ContactDto
    {
        public int Contactid { get; set; }
        public string Fullname { get; set; }
        public string Relationship { get; set; }
        public string? Address { get; set; }
        public string Phonenumber { get; set; }
        public string? TeacherName { get; set; }
    }
}