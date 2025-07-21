using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public interface ISubjectsOfExemptionService
    {
        Task<IEnumerable<Subjectsofexemption>> GetAllAsync();
        Task<(List<Subjectsofexemption> Items, int TotalCount)> GetPagedAsync(int page, int pageSize);
        Task<Subjectsofexemption?> GetByIdAsync(int id);
        Task<Subjectsofexemption> AddAsync(Subjectsofexemption exemption);
        Task<Subjectsofexemption?> UpdateAsync(int id, Subjectsofexemption exemption);
        Task<bool> DeleteAsync(int id);
    }
}
