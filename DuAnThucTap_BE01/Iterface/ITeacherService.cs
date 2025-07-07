// Interface/ITeacherService.cs
using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetAllAsync();
        Task<Teacher?> GetByIdAsync(Guid id);
        Task<Teacher> CreateAsync(Teacher teacher);
        Task<Teacher?> UpdateAsync(Guid id, Teacher teacher);
        Task<bool> DeleteAsync(Guid id);
    }
}