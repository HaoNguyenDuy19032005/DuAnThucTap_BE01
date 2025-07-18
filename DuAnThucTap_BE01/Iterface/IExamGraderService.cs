using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Interface
{
    public interface IExamGraderService
    {
        Task<PagedResponse<ExamGraderResponseDto>> GetPagedExamGradersAsync(string? searchQuery, int pageNumber, int pageSize);
        Task<ExamGraderResponseDto?> GetByIdAsync(int examGraderId);
        Task<Examgrader> CreateAsync(ExamGraderDto examGraderDto);
        Task<Examgrader?> UpdateAsync(int examGraderId, ExamGraderDto updatedExamGraderDto);
        Task<bool> DeleteAsync(int examGraderId);
    }
}
