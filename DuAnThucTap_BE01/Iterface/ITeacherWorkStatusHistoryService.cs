// Interface/ITeacherWorkStatusHistoryService.cs
using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Dtos;
using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherWorkStatusHistoryService
    {
        Task<IEnumerable<TeacherWorkStatusHistoryDto>> GetAllAsync();
        Task<TeacherWorkStatusHistoryDto?> GetByIdAsync(int id);
        Task<TeacherWorkStatusHistoryDto> CreateAsync(Teacherworkstatushistory history);
        Task<Teacherworkstatushistory?> UpdateAsync(int id, Teacherworkstatushistory history);
        Task<bool> DeleteAsync(int id);
    }
}