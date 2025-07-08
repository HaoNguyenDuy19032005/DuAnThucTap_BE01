using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetAllAsync();

        // Sửa Guid thành int
        Task<Teacher?> GetByIdAsync(int id);

        Task<Teacher> CreateAsync(Teacher teacher);

        // Sửa Guid thành int
        Task<Teacher?> UpdateAsync(int id, Teacher teacher);

        // Sửa Guid thành int
        Task<bool> DeleteAsync(int id);
    }
}