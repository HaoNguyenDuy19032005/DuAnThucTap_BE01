using DuAnThucTapNhom3.Models;

namespace DuAnThucTapNhom3.Iterface
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetAllAsync();
        Task<Teacher> GetByIdAsync(int id);
        Task<Teacher> CreateAsync(Teacher teacher);
        Task<Teacher> UpdateAsync(int id, Teacher teacher);
        Task<bool> DeleteAsync(int id);
       
    }

}
