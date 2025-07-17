using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherWorkStatusHistoryService
    {
        Task<PagedResponse<TeacherWorkStatusHistoryDto>> GetAllAsync(string? searchQuery, int pageNumber, int pageSize);

        Task<TeacherWorkStatusHistoryDto?> GetByIdAsync(int id);
        Task<TeacherWorkStatusHistoryDto> CreateAsync(TeacherWorkStatusHistoryRequestDto historyDto);
        Task<TeacherWorkStatusHistoryDto?> UpdateAsync(int id, TeacherWorkStatusHistoryRequestDto historyDto);
        Task<bool> DeleteAsync(int id);
    }
}