using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public interface IStudentSemesterSummaryService
    {
        Task<List<Studentsemestersummary>> GetAllAsync();
        Task<(List<Studentsemestersummary> Data, int TotalCount)> GetPagedAsync(int page, int pageSize);
        Task<Studentsemestersummary?> GetByIdAsync(int id);
        Task<Studentsemestersummary> AddAsync(Studentsemestersummary summary);
        Task<Studentsemestersummary?> UpdateAsync(int id, Studentsemestersummary summary);
        Task<bool> DeleteAsync(int id);
    }
}
