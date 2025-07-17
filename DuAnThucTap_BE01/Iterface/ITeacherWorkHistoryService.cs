using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherWorkHistoryService
    {
        Task<IEnumerable<TeacherWorkHistoryDto>> GetAllAsync();
        Task<TeacherWorkHistoryDto?> GetByIdAsync(int id);
        Task<Teacherworkhistory> CreateAsync(Teacherworkhistory history);
        Task<Teacherworkhistory?> UpdateAsync(int id, Teacherworkhistory history);
        Task<bool> DeleteAsync(int id);
    }
}