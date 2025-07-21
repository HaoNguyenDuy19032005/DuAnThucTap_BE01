using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Interface
{
    public interface IStudentDisciplineService
    {
        Task<IEnumerable<Studentdiscipline>> GetAllAsync();
        Task<(List<Studentdiscipline> Disciplines, int TotalCount)> GetPagedAsync(int page, int pageSize);
        Task<Studentdiscipline?> GetByIdAsync(int id);
        Task<Studentdiscipline> AddAsync(Studentdiscipline discipline);
        Task<Studentdiscipline?> UpdateAsync(int id, Studentdiscipline discipline);
        Task<bool> DeleteAsync(int id);
    }
}
