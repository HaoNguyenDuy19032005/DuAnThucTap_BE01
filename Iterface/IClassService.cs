using DuAnThucTapNhom3.Models;

namespace DuAnThucTapNhom3.Iterface
{
    public interface IClassService
    {
        Task<IEnumerable<Class>> GetAllAsync();
        Task<Class?> GetByIdAsync(int id);
        Task<Class> CreateAsync(Class newClass);
        Task<Class?> UpdateAsync(int id, Class updatedClass);
        Task<bool> DeleteAsync(int id);
    }
}
