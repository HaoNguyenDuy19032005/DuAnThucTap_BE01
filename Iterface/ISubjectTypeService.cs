using DuAnThucTapNhom3.Models;

namespace DuAnThucTapNhom3.Iterface
{
    public interface ISubjectTypeService
    {
        Task<IEnumerable<Subjecttype>> GetAllAsync();
        Task<Subjecttype?> GetByIdAsync(int id);
        Task<Subjecttype> CreateAsync(Subjecttype subjectType);
        Task<Subjecttype?> UpdateAsync(int id, Subjecttype subjectType);
        Task<bool> DeleteAsync(int id);
    }

}
