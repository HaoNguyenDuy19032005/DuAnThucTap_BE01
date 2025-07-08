using DuAnThucTapNhom3.Models;

namespace DuAnThucTapNhom3.Iterface
{
    public interface IGradeLevelService
    {
        Task<IEnumerable<Gradelevel>> GetAllAsync();
        Task<Gradelevel?> GetByIdAsync(int id);
        Task<Gradelevel> CreateAsync(Gradelevel gradeLevel);
        Task<Gradelevel?> UpdateAsync(int id, Gradelevel gradeLevel);
        Task<bool> DeleteAsync(int id);
    }
}
