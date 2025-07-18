using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using Microsoft.AspNetCore.Http; // Thêm using này

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherWorkStatusHistoryService
    {
        Task<PagedResponse<TeacherWorkStatusHistoryDto>> GetAllAsync(string? searchQuery, int pageNumber, int pageSize);
        Task<TeacherWorkStatusHistoryDto?> GetByIdAsync(int id);

        // Thêm IFormFile vào CreateAsync
        Task<TeacherWorkStatusHistoryDto> CreateAsync(TeacherWorkStatusHistoryRequestDto historyDto, IFormFile? file);

        // Thêm IFormFile vào UpdateAsync
        Task<TeacherWorkStatusHistoryDto?> UpdateAsync(int id, TeacherWorkStatusHistoryRequestDto historyDto, IFormFile? file);

        Task<bool> DeleteAsync(int id);
    }
}