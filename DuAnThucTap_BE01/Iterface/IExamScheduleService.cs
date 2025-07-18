using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Interface
{
    public interface IExamScheduleService
    {
        Task<PagedResponse<ExamScheduleResponseDto>> GetPagedExamSchedulesAsync(string? searchQuery, int pageNumber, int pageSize);
        Task<ExamScheduleResponseDto?> GetByIdAsync(int examScheduleId);
        Task<Examschedule> CreateAsync(ExamScheduleDto examScheduleDto);
        Task<Examschedule?> UpdateAsync(int examScheduleId, ExamScheduleDto updatedExamScheduleDto);
        Task<bool> DeleteAsync(int examScheduleId);
    }
}
