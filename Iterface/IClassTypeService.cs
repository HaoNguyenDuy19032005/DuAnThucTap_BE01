using DuAnThucTapNhom3.Models;

namespace DuAnThucTapNhom3.Iterface
{
    public interface IClassTypeService
    {
        Task<IEnumerable<Classtype>> GetAllAsync();
        Task<Classtype?> GetByIdAsync(int id);
        Task<Classtype> CreateAsync(Classtype classType);
        Task<Classtype?> UpdateAsync(int id, Classtype classType);
        Task<bool> DeleteAsync(int id);
    }
}
