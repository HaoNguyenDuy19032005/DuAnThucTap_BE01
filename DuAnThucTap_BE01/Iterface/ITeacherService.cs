using DuAnThucTap_BE01.Dtos; 
using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Interface
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetAllAsync();

        Task<TeacherDto?> GetByIdAsync(int id);

        Task<Teacher> CreateAsync(Teacher teacher);
        Task<Teacher?> UpdateAsync(int id, Teacher teacher);
        Task<bool> DeleteAsync(int id);
    }
}