using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public interface IDisciplinetypeService
    {
        Task<List<Disciplinetype>> GetAllAsync();
        Task<(List<Disciplinetype> Data, int TotalCount)> GetPagedAsync(int page, int pageSize);
        Task<Disciplinetype?> GetByIdAsync(int id);
        Task<Disciplinetype> AddAsync(Disciplinetype disciplinetype);
        Task<Disciplinetype?> UpdateAsync(int id, Disciplinetype disciplinetype);
        Task<bool> DeleteAsync(int id);
    }
}
