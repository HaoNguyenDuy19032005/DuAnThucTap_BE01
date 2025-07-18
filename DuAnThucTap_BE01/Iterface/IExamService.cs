using DuAnThucTap_BE01.DTO;
using DuAnThucTap_BE01.Models;
using DuAnThucTap_BE01.Response;
using System.Threading.Tasks;

namespace DuAnThucTap_BE01.Interface
{
    public interface IExamService
    {
        // THAY ĐỔI: Nhận các tham số riêng lẻ
        Task<PagedResponse<ExamResponseDto>> GetPagedExamsAsync(string? searchQuery, int pageNumber, int pageSize);

        Task<ExamResponseDto?> GetByIdAsync(int id);
        Task<Exam> CreateAsync(Exam exam);
        Task<Exam?> UpdateAsync(int id, Exam exam);
        Task<bool> DeleteAsync(int id);
    }
}
