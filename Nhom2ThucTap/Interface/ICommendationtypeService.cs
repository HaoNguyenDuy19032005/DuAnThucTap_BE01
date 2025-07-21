using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public interface ICommendationtypeService
    {
        Task<List<Commendationtype>> GetAllAsync();
        Task<(List<Commendationtype> Data, int TotalCount)> GetPagedAsync(int page, int pageSize);
        Task<Commendationtype?> GetByIdAsync(int id);
        Task<Commendationtype> AddAsync(Commendationtype commendationtype);
        Task<Commendationtype?> UpdateAsync(int id, Commendationtype commendationtype);
        Task<bool> DeleteAsync(int id);
    }
}
