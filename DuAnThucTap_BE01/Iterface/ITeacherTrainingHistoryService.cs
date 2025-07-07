using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherTrainingHistoryService
    {
        Task<IEnumerable<Teachertraininghistory>> GetAllAsync();
        Task<Teachertraininghistory?> GetByIdAsync(Guid id);
        Task<Teachertraininghistory> CreateAsync(Teachertraininghistory history);
        Task<Teachertraininghistory?> UpdateAsync(Guid id, Teachertraininghistory history);
        Task<bool> DeleteAsync(Guid id);
    }
}