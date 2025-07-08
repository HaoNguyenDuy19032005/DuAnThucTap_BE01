using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherWorkStatusHistoryService
    {
        Task<IEnumerable<Teacherworkstatushistory>> GetAllAsync();

        // Sửa Guid thành int
        Task<Teacherworkstatushistory?> GetByIdAsync(int id);

        Task<Teacherworkstatushistory> CreateAsync(Teacherworkstatushistory history);

        // Sửa Guid thành int
        Task<Teacherworkstatushistory?> UpdateAsync(int id, Teacherworkstatushistory history);

        // Sửa Guid thành int
        Task<bool> DeleteAsync(int id);
    }
}