public class StudentCreateDto
{
    public string? Fullname { get; set; }
    public string? Gender { get; set; }
    public string? Dateofbirth { get; set; }
    public string? Enrollmentdate { get; set; }
    public string? Studentcode { get; set; }
    public string? Birthplace { get; set; }
    public string? Ethnicity { get; set; }
    public string? Admissiontype { get; set; }
    public string? Email { get; set; }
    public string? Phonenumber { get; set; }
    public string? Fathername { get; set; }
    public int? Fatherbirthyear { get; set; }
    public string? Fatheroccupation { get; set; }
    public string? Phonenumberfather { get; set; }
    public string? Mothername { get; set; }
    public int? Motherbirthyear { get; set; }
    public string? Motheroccupation { get; set; }
    public string? Guardianname { get; set; }
    public int? Guardianbirthyear { get; set; }
    public string? Guardianoccupation { get; set; }
    public string? Phonenumberguardian { get; set; }
    public IFormFile? ProfileImage { get; set; }
}
