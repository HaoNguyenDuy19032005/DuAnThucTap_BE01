using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherWorkStatusHistoryService
    {
        Task<IEnumerable<Teacherworkstatushistory>> GetAllAsync();
        Task<Teacherworkstatushistory?> GetByIdAsync(Guid id);
        Task<Teacherworkstatushistory> CreateAsync(Teacherworkstatushistory history);
        Task<Teacherworkstatushistory?> UpdateAsync(Guid id, Teacherworkstatushistory history);
        Task<bool> DeleteAsync(Guid id);
    }
}