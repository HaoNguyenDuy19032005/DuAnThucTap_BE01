using DuAnThucTapNhom3.Models;

namespace DuAnThucTapNhom3.Iterface
{
    public interface ISubjectService
    {
        Task<IEnumerable<Subject>> GetAllAsync();
        Task<Subject?> GetByIdAsync(int id);
        Task<Subject> CreateAsync(Subject subject);
        Task<Subject?> UpdateAsync(int id, Subject subject);
        Task<bool> DeleteAsync(int id);
    }
}
