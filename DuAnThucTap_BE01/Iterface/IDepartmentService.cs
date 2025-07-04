using DuAnThucTap_BE01.Models;

namespace DuAnThucTap_BE01.Iterface
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task<Department> CreateAsync(Department newDepartment);
        Task<Department?> UpdateAsync(int id, Department updatedDepartment);
        Task<bool> DeleteAsync(int id);
    }
}
