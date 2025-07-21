using Nhom2ThucTap.Models;

public interface IStudentService
{
    Task<(List<Student>, int)> GetPagedStudentsAsync(int page, int pageSize, string? keyword);
    Task<Student?> GetStudentByIdAsync(int id);
    Task AddStudentAsync(Student student);
    Task UpdateStudentAsync(Student student);
    Task DeleteStudentAsync(int id);

 
    Task<bool> IsStudentCodeExistsAsync(string studentCode, int? excludeId = null);
}
