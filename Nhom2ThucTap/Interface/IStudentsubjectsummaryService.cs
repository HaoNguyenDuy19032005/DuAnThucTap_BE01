using Nhom2ThucTap.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nhom2ThucTap.Services
{
    public interface IStudentsubjectsummaryService
    {
        Task<List<Studentsubjectsummary>> GetAllAsync();
        Task<(List<Studentsubjectsummary> Summaries, int TotalCount)> GetPagedAsync(int page, int pageSize);
        Task<Studentsubjectsummary?> GetByIdAsync(int id);
        Task<Studentsubjectsummary> AddAsync(Studentsubjectsummary summary);
        Task<Studentsubjectsummary?> UpdateAsync(int id, Studentsubjectsummary summary);
        Task<bool> DeleteAsync(int id);
    }
}
