using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public interface IStudentYearlyStatusService
    {
        Task<List<Studentyearlystatus>> GetAllAsync();
        Task<(List<Studentyearlystatus> Items, int TotalCount)> GetPagedAsync(int page, int pageSize);
        Task<Studentyearlystatus?> GetByIdAsync(int id);
        Task<Studentyearlystatus?> GetByStudentIdAsync(int studentId);
        Task AddAsync(Studentyearlystatus status);
        Task<bool> UpdateAsync(Studentyearlystatus status);
        Task<bool> DeleteAsync(int id);

        Task<bool> IsValidForeignKeysAsync(Studentyearlystatus status);
    }
}
