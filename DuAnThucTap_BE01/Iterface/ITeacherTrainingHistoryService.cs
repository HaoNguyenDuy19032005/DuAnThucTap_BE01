using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using Microsoft.AspNetCore.Http;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherTrainingHistoryService
    {
        Task<PagedResponse<TeacherTrainingHistoryDto>> GetAllAsync(string? searchQuery, int pageNumber, int pageSize);
        Task<TeacherTrainingHistoryDto?> GetByIdAsync(int id);

        Task<Teachertraininghistory> CreateAsync(TeacherTrainingHistoryRequestDto historyDto, IFormFile? file);

        Task<Teachertraininghistory?> UpdateAsync(int id, TeacherTrainingHistoryRequestDto historyDto, IFormFile? file);

        Task<bool> DeleteAsync(int id);
    }
}