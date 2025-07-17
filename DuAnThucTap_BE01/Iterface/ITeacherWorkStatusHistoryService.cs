using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherWorkStatusHistoryService
    {
        Task<IEnumerable<TeacherWorkStatusHistoryDto>> GetAllAsync();
        Task<TeacherWorkStatusHistoryDto?> GetByIdAsync(int id);
        Task<TeacherWorkStatusHistoryDto> CreateAsync(TeacherWorkStatusHistoryRequestDto historyDto);
        Task<TeacherWorkStatusHistoryDto?> UpdateAsync(int id, TeacherWorkStatusHistoryRequestDto historyDto);
        Task<bool> DeleteAsync(int id);
    }
}