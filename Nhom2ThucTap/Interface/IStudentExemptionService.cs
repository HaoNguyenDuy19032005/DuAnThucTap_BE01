using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public interface IStudentExemptionService
    {
        Task<IEnumerable<Studentexemption>> GetAllAsync();
        Task<(List<Studentexemption> Exemptions, int TotalCount)> GetPagedAsync(int page, int pageSize);
        Task<Studentexemption?> GetByIdAsync(int id);
        Task<Studentexemption> AddAsync(Studentexemption exemption);
        Task<Studentexemption?> UpdateAsync(int id, Studentexemption exemption);
        Task<bool> DeleteAsync(int id);
    }
}
