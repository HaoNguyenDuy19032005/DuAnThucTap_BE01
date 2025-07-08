using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherTrainingHistoryService
    {
        Task<IEnumerable<Teachertraininghistory>> GetAllAsync();

        // Sửa Guid thành int
        Task<Teachertraininghistory?> GetByIdAsync(int id);

        Task<Teachertraininghistory> CreateAsync(Teachertraininghistory history);

        // Sửa Guid thành int
        Task<Teachertraininghistory?> UpdateAsync(int id, Teachertraininghistory history);

        // Sửa Guid thành int
        Task<bool> DeleteAsync(int id);
    }
}