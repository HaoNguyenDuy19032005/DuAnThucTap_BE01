using System.Collections.Generic;
using System.Threading.Tasks;
using DuAnThucTapNhom2.Models;

namespace DuAnThucTapNhom2.Iterface
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task<Student> CreateAsync(Student student);
        Task<Student> UpdateAsync(int id, Student student);
        Task<bool> DeleteAsync(int id);
    }
}
