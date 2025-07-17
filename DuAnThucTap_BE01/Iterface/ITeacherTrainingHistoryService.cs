using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherTrainingHistoryService
    {
        Task<IEnumerable<TeacherTrainingHistoryDto>> GetAllAsync();
        Task<TeacherTrainingHistoryDto?> GetByIdAsync(int id);
        Task<Teachertraininghistory> CreateAsync(Teachertraininghistory history);
        Task<Teachertraininghistory?> UpdateAsync(int id, Teachertraininghistory history);
        Task<bool> DeleteAsync(int id);
    }
}