using DuAnThucTap.DTOs;
using DuAnThucTap.Model;

namespace DuAnThucTap.Services
{
    public interface IGradetypeService
    {
        Task<PaginatedList<Gradetype>> GetAllAsync(string? search, int pageIndex, int pageSize);


        Task<Gradetype?> GetByIdAsync(int id);
        Task<object> CreateAsync(GradetypeDto dto);
        Task<object> UpdateAsync(int id, GradetypeDto dto);
        Task<object> DeleteAsync(int id);
    }
}
