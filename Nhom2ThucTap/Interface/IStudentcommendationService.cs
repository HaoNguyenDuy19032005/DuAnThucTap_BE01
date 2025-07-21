using Nhom2ThucTap.Models;

namespace Nhom2ThucTap.Services
{
    public interface IStudentcommendationService
    {
        Task<IEnumerable<Studentcommendation>> GetAllAsync();
        Task<(List<Studentcommendation> Commendations, int TotalCount)> GetPagedAsync(int page, int pageSize);
        Task<Studentcommendation?> GetByIdAsync(int id);
        Task<Studentcommendation> AddAsync(Studentcommendation commendation);
        Task<Studentcommendation?> UpdateAsync(int id, Studentcommendation commendation);
        Task<bool> DeleteAsync(int id);
    }
}
