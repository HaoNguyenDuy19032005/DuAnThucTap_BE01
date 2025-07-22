using DuAnThucTap.Model;

public class TeacherConcurrentSubject
{
    public int TeacherID { get; set; }
    public int SubjectID { get; set; }
    public int SchoolYearID { get; set; }

    // Navigation properties
    public Teacher Teacher { get; set; }
    public Subject Subject { get; set; }
    public Schoolyear SchoolYear { get; set; }
}

public class TeacherConcurrentSubjectDto
{
    public int TeacherID { get; set; }
    public int SubjectID { get; set; }
    public int SchoolYearID { get; set; }
}

