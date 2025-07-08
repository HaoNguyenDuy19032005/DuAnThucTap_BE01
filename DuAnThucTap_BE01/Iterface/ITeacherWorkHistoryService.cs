using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherWorkHistoryService
    {
        Task<IEnumerable<Teacherworkhistory>> GetAllAsync();

        // Sửa Guid thành int
        Task<Teacherworkhistory?> GetByIdAsync(int id);

        Task<Teacherworkhistory> CreateAsync(Teacherworkhistory history);

        // Sửa Guid thành int
        Task<Teacherworkhistory?> UpdateAsync(int id, Teacherworkhistory history);

        // Sửa Guid thành int
        Task<bool> DeleteAsync(int id);
    }
}