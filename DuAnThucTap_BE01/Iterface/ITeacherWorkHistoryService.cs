using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response; 

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherWorkHistoryService
    {
        Task<PagedResponse<TeacherWorkHistoryDto>> GetAllAsync(string? searchQuery, int pageNumber, int pageSize);

        Task<TeacherWorkHistoryDto?> GetByIdAsync(int id);
        Task<Teacherworkhistory> CreateAsync(TeacherWorkHistoryRequestDto historyDto);
        Task<Teacherworkhistory?> UpdateAsync(int id, TeacherWorkHistoryRequestDto historyDto);
        Task<bool> DeleteAsync(int id);
    }
}