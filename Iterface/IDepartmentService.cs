using DuAnThucTapNhom3.Models;

namespace DuAnThucTapNhom3.Iterface
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> GetByIdAsync(int id);
        Task<Department> CreateAsync(Department department);
        Task<Department> UpdateAsync(int id, Department department);
        Task<bool> DeleteAsync(int id);
    }

}
