using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherWorkHistoryService
    {
        Task<IEnumerable<Teacherworkhistory>> GetAllAsync();
        Task<Teacherworkhistory?> GetByIdAsync(Guid id);
        Task<Teacherworkhistory> CreateAsync(Teacherworkhistory history);
        Task<Teacherworkhistory?> UpdateAsync(Guid id, Teacherworkhistory history);
        Task<bool> DeleteAsync(Guid id);
    }
}